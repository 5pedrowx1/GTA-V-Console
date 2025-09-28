using System.Collections.Generic;
using System.IO;

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
            if (!Directory.Exists(scriptsFolder)) Directory.CreateDirectory(scriptsFolder);
            LoadFileList();
        }

        public void LoadFileList()
        {
            FileList.Clear();
            if (Directory.Exists(scriptsFolder))
            {
                var files = Directory.GetFiles(scriptsFolder, "*.cs");
                foreach (var f in files) FileList.Add(Path.GetFileName(f));
            }
        }

        public void LoadFile(string fileName, EditorBuffer buffer)
        {
            var path = Path.Combine(scriptsFolder, fileName);
            if (File.Exists(path))
            {
                buffer.CodeLines.Clear();
                buffer.CodeLines.AddRange(File.ReadAllLines(path));
                CurrentFileName = fileName;
                buffer.HasUnsavedChanges = false;
            }
        }

        public void SaveCurrentFile(EditorBuffer buffer)
        {
            var path = Path.Combine(scriptsFolder, CurrentFileName);
            File.WriteAllText(path, buffer.TextContent);
            buffer.HasUnsavedChanges = false;
        }

        public void NewFile(EditorBuffer buffer)
        {
            if (buffer.HasUnsavedChanges) return;
            buffer.InitializeTemplate();
            CurrentFileName = "untitled.cs";
        }

        public void HandleInput(System.Windows.Forms.Keys key)
        {
            if (key == System.Windows.Forms.Keys.Up) SelectedFileIndex = System.Math.Max(0, SelectedFileIndex - 1);
            if (key == System.Windows.Forms.Keys.Down) SelectedFileIndex = System.Math.Min(FileList.Count - 1, SelectedFileIndex + 1);
        }
    }
}