using GTA;
using GTA.Native;
using System;
using System.Drawing;
using System.Windows.Forms;
using Hash = GTA.Native.Hash;
using Screen = GTA.UI.Screen;
using GTA.UI;

namespace GTA_V_Console
{

    public class GTAVConsole : Script
    {
        private bool consoleVisible = false;
        private EditorBuffer buffer;
        private FileManager fileManager;
        private CompilerManager compiler;
        private ConsoleRenderer renderer;

        private EditorMode currentMode = EditorMode.Edit;
        private int scrollOffset = 0;
        private int maxVisibleLines = 25;

        public enum EditorMode
        {
            Edit,
            FileManager,
            Output
        }

        public GTAVConsole()
        {
            buffer = new EditorBuffer();
            buffer.InitializeTemplate();

            fileManager = new FileManager();
            compiler = new CompilerManager();
            renderer = new ConsoleRenderer();

            Tick += OnTick;
            KeyDown += OnKeyDown;

            Notification.Show("GTA V Console carregado! Pressione ~b~F11~w~ para abrir.");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (consoleVisible)
            {
                DrawConsole();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Toggle console
            if (e.KeyCode == Keys.F11)
            {
                consoleVisible = !consoleVisible;
                if (consoleVisible)
                {
                    fileManager.RefreshFileList();
                }
                return;
            }

            if (!consoleVisible) return;

            // Global shortcuts
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        fileManager.SaveCurrentFile(buffer);
                        renderer.ShowMessage("Arquivo salvo!", Color.Green);
                        break;
                    case Keys.O:
                        currentMode = EditorMode.FileManager;
                        break;
                    case Keys.N:
                        fileManager.NewFile(buffer);
                        currentMode = EditorMode.Edit;
                        break;
                    case Keys.R:
                        compiler.CompileAndRun(buffer, renderer);
                        currentMode = EditorMode.Output;
                        break;
                }
                return;
            }

            // Mode switching
            switch (e.KeyCode)
            {
                case Keys.F1:
                    currentMode = EditorMode.Edit;
                    break;
                case Keys.F2:
                    currentMode = EditorMode.FileManager;
                    break;
                case Keys.F3:
                    currentMode = EditorMode.Output;
                    break;
                case Keys.Escape:
                    consoleVisible = false;
                    break;
            }

            // Handle input based on current mode
            switch (currentMode)
            {
                case EditorMode.Edit:
                    HandleEditInput(e);
                    break;
                case EditorMode.FileManager:
                    HandleFileManagerInput(e);
                    break;
                case EditorMode.Output:
                    HandleOutputInput(e);
                    break;
            }
        }

        private void HandleEditInput(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    buffer.MoveCursor(-1, 0);
                    break;
                case Keys.Down:
                    buffer.MoveCursor(1, 0);
                    break;
                case Keys.Left:
                    buffer.MoveCursor(0, -1);
                    break;
                case Keys.Right:
                    buffer.MoveCursor(0, 1);
                    break;
                case Keys.Home:
                    buffer.CursorColumn = 0;
                    break;
                case Keys.End:
                    buffer.CursorColumn = buffer.GetCurrentLine().Length;
                    break;
                case Keys.PageUp:
                    scrollOffset = Math.Max(0, scrollOffset - 10);
                    break;
                case Keys.PageDown:
                    scrollOffset = Math.Min(Math.Max(0, buffer.CodeLines.Count - maxVisibleLines), scrollOffset + 10);
                    break;
                case Keys.Back:
                    buffer.HandleBackspace();
                    break;
                case Keys.Delete:
                    buffer.HandleDelete();
                    break;
                case Keys.Enter:
                    buffer.HandleEnter();
                    break;
                case Keys.Tab:
                    buffer.InsertText("    "); // 4 spaces
                    break;
            }
        }

        private void HandleFileManagerInput(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    fileManager.SelectedFileIndex = Math.Max(0, fileManager.SelectedFileIndex - 1);
                    break;
                case Keys.Down:
                    fileManager.SelectedFileIndex = Math.Min(fileManager.FileList.Count - 1, fileManager.SelectedFileIndex + 1);
                    break;
                case Keys.Enter:
                    if (fileManager.FileList.Count > 0 && fileManager.SelectedFileIndex < fileManager.FileList.Count)
                    {
                        fileManager.LoadFile(fileManager.FileList[fileManager.SelectedFileIndex], buffer);
                        currentMode = EditorMode.Edit;
                        renderer.ShowMessage($"Arquivo '{fileManager.CurrentFileName}' carregado!", Color.Green);
                    }
                    break;
                case Keys.Delete:
                    // TODO: Implementar exclusão de arquivo
                    break;
            }
        }

        private void HandleOutputInput(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    renderer.ScrollOutput(-1);
                    break;
                case Keys.Down:
                    renderer.ScrollOutput(1);
                    break;
                case Keys.Home:
                    renderer.ResetOutputScroll();
                    break;
            }
        }

        private void DrawConsole()
        {
            var screenRes = Function.Call<Size>(Hash.GET_SCREEN_RESOLUTION, 0, 0);
            var consoleX = 50f;
            var consoleY = 50f;
            var consoleWidth = screenRes.Width - 100f;
            var consoleHeight = screenRes.Height - 100f;

            // Background usando Function.Call (DirectX)
            Function.Call(Hash.DRAW_RECT,
                consoleX / screenRes.Width + consoleWidth / (2f * screenRes.Width),
                consoleY / screenRes.Height + consoleHeight / (2f * screenRes.Height),
                consoleWidth / screenRes.Width,
                consoleHeight / screenRes.Height,
                0, 0, 0, 200);

            // Title bar
            var titleText = $"GTA V C# Console - {GetModeTitle()} - {fileManager.CurrentFileName}";
            if (buffer.HasUnsavedChanges) titleText += " *";

            // Usando Function.Call para desenhar texto
            Function.Call(Hash.SET_TEXT_FONT, 0);
            Function.Call(Hash.SET_TEXT_SCALE, 0.4f, 0.4f);
            Function.Call(Hash.SET_TEXT_COLOUR, 255, 255, 255, 255);
            Function.Call(Hash.SET_TEXT_OUTLINE);
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, titleText);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, (consoleX + 10f) / screenRes.Width, (consoleY + 5f) / screenRes.Height);

            // Mode tabs
            DrawModeTabs(screenRes, consoleX, consoleY, consoleWidth);

            // Content area
            var contentY = consoleY + 50f;
            var contentHeight = consoleHeight - 80f;

            switch (currentMode)
            {
                case EditorMode.Edit:
                    DrawEditor(screenRes, consoleX, contentY, consoleWidth, contentHeight);
                    break;
                case EditorMode.FileManager:
                    DrawFileManager(screenRes, consoleX, contentY, consoleWidth, contentHeight);
                    break;
                case EditorMode.Output:
                    DrawOutput(screenRes, consoleX, contentY, consoleWidth, contentHeight);
                    break;
            }

            // Help bar
            DrawHelpBar(screenRes, consoleX, consoleY + consoleHeight - 25f);

            // Status message
            renderer.DrawStatusMessage(screenRes, consoleX + consoleWidth - 200f, consoleY + 5f);
        }

        private void DrawText(string text, float x, float y, Size screenRes, float scale = 0.3f, int r = 255, int g = 255, int b = 255, int a = 255)
        {
            Function.Call(Hash.SET_TEXT_FONT, 0);
            Function.Call(Hash.SET_TEXT_SCALE, scale, scale);
            Function.Call(Hash.SET_TEXT_COLOUR, r, g, b, a);
            Function.Call(Hash.SET_TEXT_OUTLINE);
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x / screenRes.Width, y / screenRes.Height);
        }

        private void DrawRect(float x, float y, float width, float height, Size screenRes, int r = 255, int g = 255, int b = 255, int a = 100)
        {
            Function.Call(Hash.DRAW_RECT,
                x / screenRes.Width + width / (2f * screenRes.Width),
                y / screenRes.Height + height / (2f * screenRes.Height),
                width / screenRes.Width,
                height / screenRes.Height,
                r, g, b, a);
        }

        private void DrawModeTabs(Size screenRes, float consoleX, float consoleY, float consoleWidth)
        {
            var tabWidth = 120f;
            var tabY = consoleY + 25f;
            var modes = new[] { "F1: Editor", "F2: Arquivos", "F3: Saída" };

            for (int i = 0; i < modes.Length; i++)
            {
                var tabX = consoleX + 10f + (i * tabWidth);
                var isActive = (EditorMode)i == currentMode;

                // Tab background
                if (isActive)
                    DrawRect(tabX, tabY, tabWidth - 5f, 20f, screenRes, 255, 255, 255, 100);
                else
                    DrawRect(tabX, tabY, tabWidth - 5f, 20f, screenRes, 128, 128, 128, 50);

                // Tab text
                DrawText(modes[i], tabX + 5f, tabY + 2f, screenRes, 0.3f);
            }
        }

        private void DrawEditor(Size screenRes, float consoleX, float contentY, float consoleWidth, float contentHeight)
        {
            var lineHeight = 15f;
            var startLine = Math.Max(0, buffer.CursorLine - maxVisibleLines + 5);
            var endLine = Math.Min(buffer.CodeLines.Count, startLine + maxVisibleLines);

            for (int i = startLine; i < endLine; i++)
            {
                var y = contentY + ((i - startLine) * lineHeight);
                var lineNumber = (i + 1).ToString().PadLeft(3);
                var lineText = i < buffer.CodeLines.Count ? buffer.CodeLines[i] : "";

                // Line number
                DrawText(lineNumber, consoleX + 10f, y, screenRes, 0.3f, 128, 128, 128);

                // Code line with basic syntax highlighting
                var textColor = GetSyntaxColor(lineText);
                DrawText(lineText, consoleX + 50f, y, screenRes, 0.3f, textColor.r, textColor.g, textColor.b);

                // Cursor
                if (i == buffer.CursorLine)
                {
                    var cursorX = consoleX + 50f + (buffer.CursorColumn * 6f); // Approximate character width
                    DrawRect(cursorX, y, 2f, lineHeight, screenRes, 255, 255, 255, 255);
                }
            }

            // Status
            var statusText = $"Linha: {buffer.CursorLine + 1}, Coluna: {buffer.CursorColumn + 1}";
            DrawText(statusText, consoleX + 10f, contentY + contentHeight - 30f, screenRes, 0.3f, 255, 255, 0);
        }

        private void DrawFileManager(Size screenRes, float consoleX, float contentY, float consoleWidth, float contentHeight)
        {
            var lineHeight = 20f;
            DrawText("Arquivos disponíveis:", consoleX + 10f, contentY, screenRes, 0.35f);

            for (int i = 0; i < fileManager.FileList.Count; i++)
            {
                var y = contentY + 25f + (i * lineHeight);
                var isSelected = i == fileManager.SelectedFileIndex;
                var prefix = isSelected ? "> " : "  ";

                if (isSelected)
                {
                    DrawRect(consoleX + 8f, y - 2f, consoleWidth - 16f, lineHeight, screenRes, 255, 255, 0, 50);
                }

                var color = isSelected ? (255, 255, 0) : (255, 255, 255);
                DrawText(prefix + fileManager.FileList[i], consoleX + 10f, y, screenRes, 0.35f, color.Item1, color.Item2, color.Item3);
            }

            if (fileManager.FileList.Count == 0)
            {
                DrawText("Nenhum arquivo encontrado.", consoleX + 10f, contentY + 25f, screenRes, 0.35f, 128, 128, 128);
            }
        }

        private void DrawOutput(Size screenRes, float consoleX, float contentY, float consoleWidth, float contentHeight)
        {
            var lineHeight = 15f;
            DrawText("Console de Saída:", consoleX + 10f, contentY, screenRes, 0.35f);

            var visibleLines = renderer.GetVisibleOutputLines(maxVisibleLines);
            int lineIndex = 0;

            foreach (var line in visibleLines)
            {
                var y = contentY + 25f + (lineIndex * lineHeight);
                var color = GetOutputColor(line);

                DrawText(line, consoleX + 10f, y, screenRes, 0.3f, color.r, color.g, color.b);
                lineIndex++;
            }

            if (renderer.OutputLines.Count == 0)
            {
                DrawText("Nenhuma saída ainda. Pressione Ctrl+R para compilar e executar.",
                        consoleX + 10f, contentY + 25f, screenRes, 0.35f, 128, 128, 128);
            }
        }

        private void DrawHelpBar(Size screenRes, float consoleX, float y)
        {
            var helpText = GetHelpText();
            DrawText(helpText, consoleX + 10f, y, screenRes, 0.3f, 0, 255, 255);
        }

        private (int r, int g, int b) GetSyntaxColor(string line)
        {
            var trimmed = line.TrimStart();
            if (trimmed.StartsWith("//")) return (0, 255, 0); // Green for comments
            if (trimmed.StartsWith("using ") || trimmed.Contains("class ") || trimmed.Contains("public "))
                return (100, 149, 237); // CornflowerBlue for keywords
            return (255, 255, 255); // White for normal text
        }

        private (int r, int g, int b) GetOutputColor(string line)
        {
            if (line.StartsWith("✓")) return (0, 255, 0); // Green
            if (line.StartsWith("✗")) return (255, 0, 0); // Red  
            if (line.StartsWith("Linha")) return (255, 255, 0); // Yellow
            return (255, 255, 255); // White
        }

        private string GetModeTitle()
        {
            switch (currentMode)
            {
                case EditorMode.Edit: return "Editor";
                case EditorMode.FileManager: return "Gerenciador de Arquivos";
                case EditorMode.Output: return "Saída";
                default: return "Console";
            }
        }

        private string GetHelpText()
        {
            switch (currentMode)
            {
                case EditorMode.Edit:
                    return "Ctrl+S: Salvar | Ctrl+N: Novo | Ctrl+O: Abrir | Ctrl+R: Compilar | ESC: Fechar";
                case EditorMode.FileManager:
                    return "↑↓: Navegar | Enter: Abrir | ESC: Fechar";
                case EditorMode.Output:
                    return "↑↓: Rolar | Home: Início | ESC: Fechar";
                default:
                    return "F11: Toggle Console";
            }
        }
    }
}
