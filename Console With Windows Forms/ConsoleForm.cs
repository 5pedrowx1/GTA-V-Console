using GTA;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using Control = System.Windows.Forms.Control;

namespace Console_With_Windows_Forms
{
    public class ConsoleForm : Form
    {
        #region Win32 API para arrastar janela sem borda
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        #endregion

        private readonly System.Collections.Generic.List<Script> loadedMods = new System.Collections.Generic.List<Script>();

        // Controles principais
        private RichTextBox codeBox;
        private RichTextBox outputBox;
        private Button runButton, stopButton, saveButton, closeButton;
        private Panel titleBar, toolBar, statusBar;
        private Label titleLabel, statusLabel;
        private Splitter splitter;
        private Panel codePanel, outputPanel;

        // Variáveis para arrastar
        private bool isDragging = false;
        private Point dragStartPoint;

        // Variáveis para redimensionar
        private bool isResizing = false;
        private Point resizeStartPoint;
        private Size resizeStartSize;

        public ConsoleForm()
        {
            InitializeComponent();
            SetupDragAndDrop();
            HighlightSyntax();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // ═══════════════════════════════════════
            //           CONFIGURAÇÃO DA JANELA
            // ═══════════════════════════════════════
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(25, 25, 28);
            this.Size = new Size(900, 650);
            this.MinimumSize = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Opacity = 0.96;

            // Bordas arredondadas
            this.Load += (s, e) => {
                IntPtr ptr = CreateRoundRectRgn(0, 0, Width, Height, 15, 15);
                SetWindowRgn(this.Handle, ptr, true);
            };

            // ═══════════════════════════════════════
            //              BARRA DE TÍTULO
            // ═══════════════════════════════════════
            titleBar = new Panel
            {
                Height = 35,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(30, 30, 34),
                Cursor = Cursors.SizeAll
            };

            titleLabel = new Label
            {
                Text = "🎮 GTA V Script Console",
                ForeColor = Color.FromArgb(100, 200, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 0, 0, 0)
            };

            // Botões da barra de título
            closeButton = CreateTitleButton("✕", Color.FromArgb(220, 60, 60));

            closeButton.Click += (s, e) => this.Hide();

            titleBar.Controls.AddRange(new Control[] { titleLabel, closeButton });

            // ═══════════════════════════════════════
            //              BARRA DE FERRAMENTAS
            // ═══════════════════════════════════════
            toolBar = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(35, 35, 40),
                Padding = new Padding(10, 8, 10, 8)
            };

            runButton = CreateStyledButton("▶ Executar", Color.FromArgb(40, 160, 40), Color.White);
            stopButton = CreateStyledButton("⬛ Parar", Color.FromArgb(180, 40, 40), Color.White);
            saveButton = CreateStyledButton("💾 Salvar", Color.FromArgb(40, 120, 180), Color.White);

            runButton.Click += RunButton_Click;
            stopButton.Click += StopButton_Click;
            saveButton.Click += SaveButton_Click;

            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Dock = DockStyle.Left,
                WrapContents = false
            };
            buttonPanel.Controls.AddRange(new Control[] { runButton, stopButton, saveButton });

            toolBar.Controls.Add(buttonPanel);

            // ═══════════════════════════════════════
            //            PAINEL PRINCIPAL
            // ═══════════════════════════════════════
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(20, 20, 24),
                Padding = new Padding(5)
            };

            // Painel do código
            codePanel = new Panel
            {
                Height = 350,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(30, 30, 35),
                Padding = new Padding(2)
            };

            codeBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(25, 25, 30),
                ForeColor = Color.White,
                Font = new Font("Cascadia Code", 10, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                AcceptsTab = true,
                DetectUrls = false,
                WordWrap = false,
                Text = GetCodeTemplate()
            };

            codePanel.Controls.Add(codeBox);

            // Splitter para redimensionar painéis
            splitter = new Splitter
            {
                Dock = DockStyle.Top,
                Height = 6,
                BackColor = Color.FromArgb(50, 50, 60),
                Cursor = Cursors.HSplit
            };

            // Painel de saída
            outputPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(20, 20, 25),
                Padding = new Padding(2)
            };

            outputBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 15, 20),
                ForeColor = Color.FromArgb(100, 255, 100),
                Font = new Font("Cascadia Code", 9),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                Text = "🚀 Console pronto! Digite seu código C# e pressione Executar.\n"
            };

            outputPanel.Controls.Add(outputBox);

            mainPanel.Controls.AddRange(new Control[] { outputPanel, splitter, codePanel });

            // ═══════════════════════════════════════
            //              BARRA DE STATUS
            // ═══════════════════════════════════════
            statusBar = new Panel
            {
                Height = 25,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(30, 30, 34)
            };

            statusLabel = new Label
            {
                Text = "Pronto",
                ForeColor = Color.FromArgb(150, 150, 150),
                Font = new Font("Segoe UI", 8),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 0, 0, 0)
            };

            // Indicador de redimensionamento
            var resizeGrip = new Label
            {
                Text = "⋰",
                Width = 20,
                ForeColor = Color.FromArgb(80, 80, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Right,
                Cursor = Cursors.SizeNWSE
            };

            statusBar.Controls.AddRange(new Control[] { statusLabel, resizeGrip });

            // Adiciona controles à janela principal
            this.Controls.AddRange(new Control[] { mainPanel, statusBar, toolBar, titleBar });

            // ═══════════════════════════════════════
            //                EVENTOS
            // ═══════════════════════════════════════
            codeBox.TextChanged += (s, e) => {
                HighlightSyntax();
                statusLabel.Text = $"Linhas: {codeBox.Lines.Length} | Caracteres: {codeBox.Text.Length}";
            };

            // Eventos para redimensionar
            resizeGrip.MouseDown += ResizeGrip_MouseDown;
            resizeGrip.MouseMove += ResizeGrip_MouseMove;
            resizeGrip.MouseUp += ResizeGrip_MouseUp;

            // Eventos para arrastar pela barra de título
            titleBar.MouseDown += TitleBar_MouseDown;
            titleBar.MouseMove += TitleBar_MouseMove;
            titleBar.MouseUp += TitleBar_MouseUp;
            titleLabel.MouseDown += TitleBar_MouseDown;
            titleLabel.MouseMove += TitleBar_MouseMove;
            titleLabel.MouseUp += TitleBar_MouseUp;

            ResumeLayout(false);
        }

        #region Métodos para criar botões estilizados

        private Button CreateTitleButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                Size = new Size(30, 25),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Dock = DockStyle.Right,
                Margin = new Padding(2),
                TabStop = false,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        private Button CreateStyledButton(string text, Color backColor, Color foreColor)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(100, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                ForeColor = foreColor,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.2f);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.1f);

            return btn;
        }

        #endregion

        #region Eventos de arrastar janela

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
                if (sender == titleLabel) dragStartPoint.X += titleLabel.Left;
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - dragStartPoint.X;
                newLocation.Y += e.Y - dragStartPoint.Y;
                if (sender == titleLabel)
                {
                    newLocation.X += titleLabel.Left;
                    newLocation.Y += titleLabel.Top;
                }
                this.Location = newLocation;
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        #endregion

        #region Eventos de redimensionar janela

        private void ResizeGrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResizing = true;
                resizeStartPoint = Cursor.Position;
                resizeStartSize = this.Size;
            }
        }

        private void ResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                int deltaX = Cursor.Position.X - resizeStartPoint.X;
                int deltaY = Cursor.Position.Y - resizeStartPoint.Y;

                Size newSize = new Size(
                    Math.Max(MinimumSize.Width, resizeStartSize.Width + deltaX),
                    Math.Max(MinimumSize.Height, resizeStartSize.Height + deltaY)
                );

                this.Size = newSize;

                // Atualiza bordas arredondadas
                IntPtr ptr = CreateRoundRectRgn(0, 0, Width, Height, 15, 15);
                SetWindowRgn(this.Handle, ptr, true);
            }
        }

        private void ResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
        }

        #endregion

        #region Funcionalidades do Console (mantido do original)

        private void RunButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Compilando...";
            outputBox.Clear();
            outputBox.AppendText("⏳ Compilando e executando script...\r\n", Color.Yellow);

            try
            {
                var code = codeBox.Text;
                var syntaxTree = CSharpSyntaxTree.ParseText(code);

                var references = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                    .Where(a => !a.GetName().Name.Equals("ScriptHookVDotNet2"))
                    .Select(a => MetadataReference.CreateFromFile(a.Location))
                    .ToList();

                var compilation = CSharpCompilation.Create(
                    assemblyName: "DynamicMod_" + DateTime.Now.Ticks,
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                using (var ms = new MemoryStream())
                {
                    var result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        outputBox.AppendText("❌ Erros de compilação:\r\n", Color.Red);
                        foreach (var diag in result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
                            outputBox.AppendText(diag.ToString() + "\r\n", Color.Orange);
                        statusLabel.Text = "Erro de compilação";
                        return;
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());

                    var scriptType = assembly.GetTypes()
                                             .FirstOrDefault(t => typeof(Script).IsAssignableFrom(t));

                    if (scriptType == null)
                    {
                        outputBox.AppendText("❌ Nenhuma classe Script encontrada!\r\n", Color.Red);
                        statusLabel.Text = "Script inválido";
                        return;
                    }

                    var scriptInstance = (Script)Activator.CreateInstance(scriptType);
                    loadedMods.Add(scriptInstance);

                    outputBox.AppendText("✅ Script carregado e executando!\r\n", Color.Lime);
                    statusLabel.Text = $"Script ativo ({loadedMods.Count} carregados)";
                }
            }
            catch (Exception ex)
            {
                outputBox.AppendText("❌ Erro: " + ex.Message + "\r\n", Color.Red);
                statusLabel.Text = "Erro na execução";
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            foreach (var mod in loadedMods)
            {
                try { mod.Abort(); } catch { }
            }
            loadedMods.Clear();
            outputBox.AppendText("🛑 Todos os scripts foram parados.\r\n", Color.Orange);
            statusLabel.Text = "Scripts parados";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string modsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            if (!Directory.Exists(modsPath)) Directory.CreateDirectory(modsPath);

            string file = Path.Combine(modsPath, "Script_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".cs");
            File.WriteAllText(file, codeBox.Text);
            outputBox.AppendText($"💾 Script salvo: {Path.GetFileName(file)}\r\n", Color.Cyan);
            statusLabel.Text = "Script salvo";
        }

        #endregion

        #region Syntax Highlighting e Template

        private void HighlightSyntax()
        {
            int selectionStart = codeBox.SelectionStart;
            int selectionLength = codeBox.SelectionLength;

            codeBox.SuspendLayout();
            codeBox.SelectAll();
            codeBox.SelectionColor = Color.White;

            var code = codeBox.Text;
            try
            {
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetRoot();

                foreach (var token in root.DescendantTokens())
                {
                    var start = token.SpanStart;
                    var length = token.Span.Length;

                    Color color = Color.White;

                    if (token.IsKeyword())
                        color = Color.FromArgb(86, 156, 214); // Azul VS
                    else if (token.IsKind(SyntaxKind.StringLiteralToken))
                        color = Color.FromArgb(206, 145, 120); // Laranja
                    else if (token.IsKind(SyntaxKind.NumericLiteralToken))
                        color = Color.FromArgb(181, 206, 168); // Verde claro
                    else if (token.IsKind(SyntaxKind.SingleLineCommentTrivia) ||
                             token.IsKind(SyntaxKind.MultiLineCommentTrivia))
                        color = Color.FromArgb(106, 153, 85); // Verde comentário

                    if (start < codeBox.Text.Length && start + length <= codeBox.Text.Length)
                    {
                        codeBox.Select(start, length);
                        codeBox.SelectionColor = color;
                    }
                }
            }
            catch { /* Ignora erros de parsing */ }

            codeBox.SelectionStart = selectionStart;
            codeBox.SelectionLength = selectionLength;
            codeBox.SelectionColor = Color.White;
            codeBox.ResumeLayout();
        }

        private string GetCodeTemplate()
        {
            return @"using System;
using GTA;          
using GTA.Native;
using GTA.UI;
using System.Windows.Forms;

public class MeuMod : Script
{
    public MeuMod()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        
        // Mensagem de inicialização
        Notification.Show(""~g~Mod carregado!~w~ Pressione ~b~F5~w~ para testar."");
    }

    private void OnTick(object sender, EventArgs e)
    {
        // Código executado a cada frame
        // Exemplo: mostrar coordenadas do jogador
        var player = Game.Player.Character;
        UI.ShowSubtitle($""Posição: {player.Position}"", 100);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            // Exemplo: criar explosão
            var pos = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5f;
            World.AddExplosion(pos, ExplosionType.Car, 1f, 1f);
            
            Notification.Show(""~r~BOOM!"");
        }
    }
}";
        }

        #endregion

        #region Métodos auxiliares

        private void SetupDragAndDrop()
        {
            // Permite arrastar arquivos .cs para o editor
            this.AllowDrop = true;
            codeBox.AllowDrop = true;

            codeBox.DragEnter += (s, e) => {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
            };

            codeBox.DragDrop += (s, e) => {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && files[0].EndsWith(".cs"))
                {
                    try
                    {
                        codeBox.Text = File.ReadAllText(files[0]);
                        outputBox.AppendText($"📂 Arquivo carregado: {Path.GetFileName(files[0])}\r\n", Color.Cyan);
                    }
                    catch (Exception ex)
                    {
                        outputBox.AppendText($"❌ Erro ao carregar arquivo: {ex.Message}\r\n", Color.Red);
                    }
                }
            };
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // Mantém a janela dentro da tela
            var screen = Screen.FromControl(this);
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x + width > screen.WorkingArea.Width) x = screen.WorkingArea.Width - width;
            if (y + height > screen.WorkingArea.Height) y = screen.WorkingArea.Height - height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        #endregion
    }

    #region Extensões para RichTextBox colorido
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
        }
    }
    #endregion
}