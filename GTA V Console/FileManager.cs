using System;
using System.Collections.Generic;
using System.IO;
using GTA.UI;

namespace GTA_V_Console
{
    public class FileManager
    {
        public List<string> FileList { get; private set; } = new List<string>();
        public int SelectedFileIndex { get; set; } = 0;
        public string CurrentFileName { get; private set; } = "untitled.cs";
        private string scriptsFolder = "scripts/user_scripts/";

        public FileManager()
        {
            try
            {
                if (!Directory.Exists(scriptsFolder))
                    Directory.CreateDirectory(scriptsFolder);
                RefreshFileList();
            }
            catch (Exception ex)
            {
                Screen.ShowSubtitle($"Erro ao inicializar FileManager: {ex.Message}");
            }
        }

        public void RefreshFileList()
        {
            try
            {
                FileList.Clear();
                if (Directory.Exists(scriptsFolder))
                {
                    var files = Directory.GetFiles(scriptsFolder, "*.cs");
                    foreach (var file in files)
                    {
                        FileList.Add(Path.GetFileName(file));
                    }
                }

                // Ajustar índice selecionado se necessário
                if (SelectedFileIndex >= FileList.Count)
                    SelectedFileIndex = Math.Max(0, FileList.Count - 1);
            }
            catch (Exception ex)
            {
                Screen.ShowSubtitle($"Erro ao carregar lista de arquivos: {ex.Message}");
            }
        }

        public void LoadFile(string fileName, EditorBuffer buffer)
        {
            try
            {
                var path = Path.Combine(scriptsFolder, fileName);
                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);
                    buffer.CodeLines.Clear();
                    buffer.CodeLines.AddRange(content.Split(new[] { '\n', '\r' }, StringSplitOptions.None));

                    // Remover linhas vazias do final
                    while (buffer.CodeLines.Count > 0 && string.IsNullOrEmpty(buffer.CodeLines[buffer.CodeLines.Count - 1]))
                        buffer.CodeLines.RemoveAt(buffer.CodeLines.Count - 1);

                    CurrentFileName = fileName;
                    buffer.HasUnsavedChanges = false;
                    buffer.CursorLine = 0;
                    buffer.CursorColumn = 0;
                }
            }
            catch (Exception ex)
            {
                Screen.ShowSubtitle($"Erro ao carregar arquivo: {ex.Message}");
            }
        }

        public void SaveCurrentFile(EditorBuffer buffer)
        {
            try
            {
                var path = Path.Combine(scriptsFolder, CurrentFileName);
                File.WriteAllText(path, buffer.GetAllText());
                buffer.HasUnsavedChanges = false;
                RefreshFileList(); // Atualizar lista caso seja um arquivo novo
            }
            catch (Exception ex)
            {
                Screen.ShowSubtitle($"Erro ao salvar arquivo: {ex.Message}");
            }
        }

        public void NewFile(EditorBuffer buffer)
        {
            if (buffer.HasUnsavedChanges)
            {
                Screen.ShowSubtitle("Salve o arquivo atual primeiro!");
                return;
            }

            buffer.InitializeTemplate();
            CurrentFileName = $"script_{DateTime.Now:yyyyMMdd_HHmmss}.cs";
        }
    }
}