using System;
using System.Collections.Generic;

namespace GTA_V_Console
{
    public class EditorBuffer
    {
        public List<string> CodeLines { get; private set; } = new List<string>();
        public bool HasUnsavedChanges { get; set; } = false;
        public int CursorLine { get; set; } = 0;
        public int CursorColumn { get; set; } = 0;

        public void InitializeTemplate()
        {
            CodeLines.Clear();
            CodeLines.AddRange(new[]
            {
                "using GTA;",
                "using GTA.Native;",
                "using System;",
                "",
                "public class UserScript : Script",
                "{",
                "    public UserScript()",
                "    {",
                "        Tick += OnTick;",
                "    }",
                "",
                "    private void OnTick(object sender, EventArgs e)",
                "    {",
                "        // Seu código aqui",
                "    }",
                "}"
            });
            CursorLine = 13;
            CursorColumn = 8;
            HasUnsavedChanges = false;
        }

        public void MoveCursor(int deltaLine, int deltaColumn)
        {
            CursorLine = Math.Max(0, Math.Min(CodeLines.Count - 1, CursorLine + deltaLine));
            CursorColumn = Math.Max(0, Math.Min(GetCurrentLine().Length, CursorColumn + deltaColumn));
        }

        public string GetCurrentLine()
        {
            return CursorLine < CodeLines.Count ? CodeLines[CursorLine] : "";
        }

        public void InsertText(string text)
        {
            var currentLine = GetCurrentLine();
            var newLine = currentLine.Insert(CursorColumn, text);
            CodeLines[CursorLine] = newLine;
            CursorColumn += text.Length;
            HasUnsavedChanges = true;
        }

        public void HandleBackspace()
        {
            if (CursorColumn > 0)
            {
                var currentLine = GetCurrentLine();
                CodeLines[CursorLine] = currentLine.Remove(CursorColumn - 1, 1);
                CursorColumn--;
                HasUnsavedChanges = true;
            }
            else if (CursorLine > 0)
            {
                var currentLine = GetCurrentLine();
                CursorColumn = CodeLines[CursorLine - 1].Length;
                CodeLines[CursorLine - 1] += currentLine;
                CodeLines.RemoveAt(CursorLine);
                CursorLine--;
                HasUnsavedChanges = true;
            }
        }

        public void HandleDelete()
        {
            var currentLine = GetCurrentLine();
            if (CursorColumn < currentLine.Length)
            {
                CodeLines[CursorLine] = currentLine.Remove(CursorColumn, 1);
                HasUnsavedChanges = true;
            }
            else if (CursorLine < CodeLines.Count - 1)
            {
                CodeLines[CursorLine] += CodeLines[CursorLine + 1];
                CodeLines.RemoveAt(CursorLine + 1);
                HasUnsavedChanges = true;
            }
        }

        public void HandleEnter()
        {
            var currentLine = GetCurrentLine();
            var beforeCursor = currentLine.Substring(0, CursorColumn);
            var afterCursor = currentLine.Substring(CursorColumn);

            CodeLines[CursorLine] = beforeCursor;
            CodeLines.Insert(CursorLine + 1, afterCursor);
            CursorLine++;
            CursorColumn = 0;
            HasUnsavedChanges = true;
        }

        public string GetAllText()
        {
            return string.Join("\n", CodeLines);
        }
    }
}
