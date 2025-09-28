using System;
using System.Collections.Generic;

namespace GTA_V_Console
{
    public class EditorBuffer
    {
        public List<string> CodeLines { get; private set; } = new List<string>();
        public bool HasUnsavedChanges { get; set; } = false;
        public int CursorLine { get; private set; } = 0;
        public int CursorColumn { get; private set; } = 0;
        public int OutputScroll { get; private set; } = 0;

        public string TextContent
        {
            get => string.Join("\n", CodeLines);
            set
            {
                CodeLines = new List<string>(value.Split('\n'));
                HasUnsavedChanges = true;
            }
        }

        public void InitializeTemplate()
        {
            CodeLines.Clear();
            CodeLines.Add("using GTA;");
            CodeLines.Add("using GTA.Native;");
            CodeLines.Add("using System;");
            CodeLines.Add("");
            CodeLines.Add("public class UserScript : Script");
            CodeLines.Add("{");
            CodeLines.Add("    public UserScript()");
            CodeLines.Add("    {");
            CodeLines.Add("        Tick += OnTick;");
            CodeLines.Add("    }");
            CodeLines.Add("");
            CodeLines.Add("    private void OnTick(object sender, EventArgs e)");
            CodeLines.Add("    {");
            CodeLines.Add("        // Seu código aqui");
            CodeLines.Add("    }");
            CodeLines.Add("}");
        }

        public void HandleEditInput(System.Windows.Forms.Keys key)
        {
            switch (key)
            {
                case System.Windows.Forms.Keys.Up:
                    CursorLine = Math.Max(0, CursorLine - 1);
                    CursorColumn = Math.Min(CursorColumn, CodeLines[CursorLine].Length);
                    break;
                case System.Windows.Forms.Keys.Down:
                    CursorLine = Math.Min(CodeLines.Count - 1, CursorLine + 1);
                    CursorColumn = Math.Min(CursorColumn, CodeLines[CursorLine].Length);
                    break;
                case System.Windows.Forms.Keys.Left:
                    CursorColumn = Math.Max(0, CursorColumn - 1);
                    break;
                case System.Windows.Forms.Keys.Right:
                    CursorColumn = Math.Min(CodeLines[CursorLine].Length, CursorColumn + 1);
                    break;
                case System.Windows.Forms.Keys.Back:
                    if (CursorColumn > 0)
                    {
                        CodeLines[CursorLine] = CodeLines[CursorLine].Remove(CursorColumn - 1, 1);
                        CursorColumn--;
                    }
                    else if (CursorLine > 0)
                    {
                        CursorColumn = CodeLines[CursorLine - 1].Length;
                        CodeLines[CursorLine - 1] += CodeLines[CursorLine];
                        CodeLines.RemoveAt(CursorLine);
                        CursorLine--;
                    }
                    break;
                case System.Windows.Forms.Keys.Enter:
                    var rest = CodeLines[CursorLine].Substring(CursorColumn);
                    CodeLines[CursorLine] = CodeLines[CursorLine].Substring(0, CursorColumn);
                    CodeLines.Insert(CursorLine + 1, rest);
                    CursorLine++;
                    CursorColumn = 0;
                    break;
            }
            HasUnsavedChanges = true;
        }

        public void HandleOutputScroll(System.Windows.Forms.Keys key)
        {
            if (key == System.Windows.Forms.Keys.Up)
                OutputScroll = Math.Max(0, OutputScroll - 1);
            else if (key == System.Windows.Forms.Keys.Down)
                OutputScroll = OutputScroll + 1;
        }
    }
}
