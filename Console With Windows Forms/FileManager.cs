using System;
using System.Collections.Generic;
using System.IO;

public class FileManager
{
    public string BasePath { get; }
    public List<string> FileList { get; private set; } = new List<string>();
    public int SelectedFileIndex { get; set; } = 0;
    public string CurrentFileName { get; private set; } = "";

    public FileManager(string baseFolder = "scripts\\InGameMods")
    {
        BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);
        Directory.CreateDirectory(BasePath);
        RefreshFileList();
    }

    public void RefreshFileList()
    {
        FileList.Clear();
        foreach (var f in Directory.GetFiles(BasePath, "*.cs"))
            FileList.Add(Path.GetFileName(f));
        if (FileList.Count == 0) SelectedFileIndex = 0;
    }

    public string LoadFile(string filename)
    {
        var path = Path.Combine(BasePath, filename);
        if (!File.Exists(path)) return null;
        CurrentFileName = filename;
        return File.ReadAllText(path);
    }

    public void SaveFile(string filename, string content)
    {
        var path = Path.Combine(BasePath, filename);
        File.WriteAllText(path, content);
        CurrentFileName = filename;
        RefreshFileList();
    }

    public string NewFile(string defaultName = "NewMod.cs", string template = null)
    {
        string name = defaultName;
        int i = 1;
        while (File.Exists(Path.Combine(BasePath, name)))
        {
            name = Path.GetFileNameWithoutExtension(defaultName) + i + ".cs";
            i++;
        }
        SaveFile(name, template ?? "");
        RefreshFileList();
        SelectedFileIndex = FileList.IndexOf(name);
        return name;
    }
}