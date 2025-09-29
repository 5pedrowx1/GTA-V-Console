using GTA;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace Console_With_Windows_Forms
{
    public class ConsoleForm : Form
    {
        private readonly System.Collections.Generic.List<Script> loadedMods = new System.Collections.Generic.List<Script>();
        private readonly RichTextBox codeBox;
        private readonly TextBox outputBox;
        private readonly Button runButton;
        private readonly Button stopButton;
        private readonly Button saveButton;

        public ConsoleForm()
        {
            // 🎨 Aparência estilo GTA V
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.FromArgb(15, 15, 15);
            this.Opacity = 0.95;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 50);
            this.Size = new Size(800, 550);

            // 🧩 Caixa de código com highlight
            codeBox = new RichTextBox
            {
                Multiline = true,
                Dock = DockStyle.Top,
                Height = 300,
                BackColor = Color.FromArgb(20, 20, 20),
                ForeColor = Color.White,
                Font = new Font("Consolas", 11),
                BorderStyle = BorderStyle.None,
                AcceptsTab = true,
                // Template inicial
                Text = GetCodeTemplate()
            };

            // 🧭 Botões
            runButton = new Button
            {
                Text = "▶ Executar",
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(30, 120, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            stopButton = new Button
            {
                Text = "⛔ Parar",
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(120, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            saveButton = new Button
            {
                Text = "💾 Salvar Script",
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(30, 30, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // 🖥️ Console de saída
            outputBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.FromArgb(25, 25, 25),
                ForeColor = Color.Lime,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.None
            };

            // Adiciona controles
            Controls.Add(outputBox);
            Controls.Add(saveButton);
            Controls.Add(stopButton);
            Controls.Add(runButton);
            Controls.Add(codeBox);

            // 🎯 Eventos
            runButton.Click += RunButton_Click;
            stopButton.Click += StopButton_Click;
            saveButton.Click += SaveButton_Click;
            codeBox.TextChanged += (s, e) => HighlightSyntax();

            // Primeira coloração
            HighlightSyntax();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            outputBox.Text = "⏳ Compilando e executando script...\r\n";

            try
            {
                // Código do usuário
                var code = codeBox.Text;

                // Cria árvore de sintaxe
                var syntaxTree = CSharpSyntaxTree.ParseText(code);

                // Referências necessárias (ScriptHook e assemblies atuais)
                var references = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                    .Where(a => !a.GetName().Name.Equals("ScriptHookVDotNet2")) // ignora a versão antiga
                    .Select(a => MetadataReference.CreateFromFile(a.Location))
                    .ToList();

                // Compilação dinâmica
                var compilation = CSharpCompilation.Create(
                    assemblyName: "DynamicMod_" + DateTime.Now.Ticks,
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                using (var ms = new System.IO.MemoryStream())
                {
                    var result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        outputBox.AppendText("❌ Erros de compilação:\r\n");
                        foreach (var diag in result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
                            outputBox.AppendText(diag.ToString() + "\r\n");
                        return;
                    }

                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());

                    // Procura classe que herda de Script
                    var scriptType = assembly.GetTypes()
                                             .FirstOrDefault(t => typeof(Script).IsAssignableFrom(t));

                    if (scriptType == null)
                    {
                        outputBox.AppendText("❌ Nenhuma classe Script encontrada!\r\n");
                        return;
                    }

                    // Cria instância e adiciona à lista para controlar depois
                    var scriptInstance = (Script)Activator.CreateInstance(scriptType);
                    loadedMods.Add(scriptInstance);

                    outputBox.AppendText("✅ Script carregado e rodando!\r\n");
                }
            }
            catch (Exception ex)
            {
                outputBox.AppendText("❌ Erro ao rodar script: " + ex.Message + "\r\n");
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            foreach (var mod in loadedMods)
            {
                try
                {
                    mod.Abort();
                }
                catch { }
            }
            loadedMods.Clear();
            outputBox.AppendText("🛑 Todos os scripts foram parados.\r\n");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string modsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            if (!Directory.Exists(modsPath))
                Directory.CreateDirectory(modsPath);

            string file = Path.Combine(modsPath, "Script_" + DateTime.Now.Ticks + ".cs");
            File.WriteAllText(file, codeBox.Text);
            outputBox.AppendText($"💾 Script salvo em: {file}\r\n");
        }

        // 🧠 Gera um template de script base
        private string GetCodeTemplate()
        {
            return
@"using System;
using GTA;          
using GTA.Native;
using GTA.UI;
using System.Windows.Forms;

public class MeuMod : Script
{
    public MeuMod()
    {
        // Intervalo do tick do script (em ms)
        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnTick(object sender, EventArgs e)
    {
        // Código que roda a cada tick do jogo
        // Exemplo: exibir mensagem constante na tela
        UI.ShowSubtitle(""Mod ativo! Pressione F5 para fazer algo."", 1000);

        // Exemplo: criar um ped na frente do jogador
        /*
        Ped player = Game.Player.Character;
        Vector3 spawnPos = player.Position + player.ForwardVector * 5f;
        Ped newPed = World.CreatePed(PedHash.Business01AMM, spawnPos);
        */
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        // Detecta tecla pressionada
        if (e.KeyCode == Keys.F5)
        {
            // Exemplo: criar explosão na frente do jogador
            Vector3 pos = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5f;
            World.AddExplosion(pos, ExplosionType.Car, 1f, 1f);
            UI.ShowSubtitle(""BOOM!"", 2000);
        }
    }
}";
        }

        // 🎨 Syntax Highlighting com Roslyn
        private void HighlightSyntax()
        {
            int selectionStart = codeBox.SelectionStart;
            int selectionLength = codeBox.SelectionLength;

            codeBox.SuspendLayout();
            codeBox.SelectAll();
            codeBox.SelectionColor = Color.White;

            var code = codeBox.Text;
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            foreach (var token in root.DescendantTokens())
            {
                var start = token.SpanStart;
                var length = token.Span.Length;

                Color color = Color.White;

                if (token.IsKeyword())
                    color = Color.DeepSkyBlue;
                else if (token.IsKind(SyntaxKind.StringLiteralToken))
                    color = Color.FromArgb(255, 206, 80);
                else if (token.IsKind(SyntaxKind.NumericLiteralToken))
                    color = Color.FromArgb(120, 180, 255);
                else if (token.IsKind(SyntaxKind.SingleLineCommentTrivia) ||
                         token.IsKind(SyntaxKind.MultiLineCommentTrivia))
                    color = Color.FromArgb(100, 180, 100);
                else if (token.IsKind(SyntaxKind.IdentifierToken))
                    color = Color.WhiteSmoke;

                codeBox.Select(start, length);
                codeBox.SelectionColor = color;
            }

            codeBox.SelectionStart = selectionStart;
            codeBox.SelectionLength = selectionLength;
            codeBox.SelectionColor = Color.White;
            codeBox.ResumeLayout();
        }
    }
}