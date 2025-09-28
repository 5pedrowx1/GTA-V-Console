using GTA;
using ImGuiNET;
using System;

namespace GTA_V_Console
{
    public class CSharpImGuiEditor : Script
    {
        private bool editorOpen = false;
        private EditorBuffer buffer;
        private FileManager fileManager;
        private CompilerManager compiler;
        private Renderer renderer;

        public struct Vector2f
        {
            public float X;
            public float Y;
            public Vector2f(float x, float y) { X = x; Y = y; }
        }

        public CSharpImGuiEditor()
        {
            buffer = new EditorBuffer();
            buffer.InitializeTemplate();

            fileManager = new FileManager();
            compiler = new CompilerManager(buffer);
            renderer = new Renderer(buffer, fileManager, compiler);

            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (editorOpen)
            {
                DrawImGuiEditor();
            }
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F11)
            {
                editorOpen = !editorOpen;
            }

            if (!editorOpen) return;

            switch (renderer.CurrentMode)
            {
                case CSharpCodeEditor.EditorMode.Edit:
                    buffer.HandleEditInput(e.KeyCode);
                    break;
                case CSharpCodeEditor.EditorMode.FileManager:
                    fileManager.HandleInput(e.KeyCode);
                    break;
                case CSharpCodeEditor.EditorMode.Output:
                    buffer.HandleOutputScroll(e.KeyCode);
                    break;
            }

            // Atalhos globais
            if (e.Control)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.S) fileManager.SaveCurrentFile(buffer);
                if (e.KeyCode == System.Windows.Forms.Keys.O) renderer.CurrentMode = CSharpCodeEditor.EditorMode.FileManager;
                if (e.KeyCode == System.Windows.Forms.Keys.N) fileManager.NewFile(buffer);
            }
        }

        private void DrawImGuiEditor()
        {
            ImGui.Begin($"GTA V C# Editor - {fileManager.CurrentFileName}", ref editorOpen, ImGuiWindowFlags.AlwaysAutoResize);

            // Modo de edição
            if (renderer.CurrentMode == CSharpCodeEditor.EditorMode.Edit)
            {
                ImGui.Text("Editor de Código:");

                // Criar uma variável temporária para evitar erro de ref em propriedades
                string tempText = buffer.TextContent;

                Vector2f size = new Vector2f(700f, 400f);
                ImGui.InputTextMultiline("##editor", ref tempText, 100000, new System.Numerics.Vector2(size.X, size.Y));

                // Exibir posição do cursor
                ImGui.Text($"Linha: {buffer.CursorLine + 1}, Coluna: {buffer.CursorColumn + 1}");
            }

            // Gerenciador de arquivos
            if (renderer.CurrentMode == CSharpCodeEditor.EditorMode.FileManager)
            {
                ImGui.Text("Gerenciador de Arquivos:");
                for (int i = 0; i < fileManager.FileList.Count; i++)
                {
                    if (ImGui.Selectable(fileManager.FileList[i], i == fileManager.SelectedFileIndex))
                    {
                        fileManager.SelectedFileIndex = i;
                        fileManager.LoadFile(fileManager.FileList[i], buffer);
                        renderer.CurrentMode = CSharpCodeEditor.EditorMode.Edit;
                    }
                }
            }

            // Output de compilação
            if (renderer.CurrentMode == CSharpCodeEditor.EditorMode.Output)
            {
                ImGui.Text("Console de Saída:");

                // Usar GetVisibleOutputLines para rolagem
                foreach (var line in renderer.GetVisibleOutputLines())
                {
                    ImGui.TextUnformatted(line);
                }
            }

            // Botões rápidos
            if (ImGui.Button("Compilar & Executar"))
                compiler.CompileAndRun(buffer, renderer);

            ImGui.SameLine();
            if (ImGui.Button("Salvar"))
                fileManager.SaveCurrentFile(buffer);

            ImGui.SameLine();
            if (ImGui.Button("Novo Arquivo"))
                fileManager.NewFile(buffer);

            ImGui.End();
        }
    }
}