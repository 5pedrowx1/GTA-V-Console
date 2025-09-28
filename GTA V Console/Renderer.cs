using System.Collections.Generic;
using System.Linq;

namespace GTA_V_Console
{
    public class Renderer
    {
        public CSharpCodeEditor.EditorMode CurrentMode = CSharpCodeEditor.EditorMode.Edit;
        public List<string> OutputLines { get; private set; } = new List<string>();

        private EditorBuffer buffer;
        private FileManager fileManager;
        private CompilerManager compiler;

        private const int MaxVisibleLines = 20;

        public Renderer(EditorBuffer buffer, FileManager fm, CompilerManager compiler)
        {
            this.buffer = buffer;
            this.fileManager = fm;
            this.compiler = compiler;
        }

        public IEnumerable<string> GetVisibleOutputLines()
        {
            int scroll = buffer.OutputScroll;
            int totalLines = OutputLines.Count;

            if (scroll > totalLines - MaxVisibleLines)
                scroll = System.Math.Max(0, totalLines - MaxVisibleLines);

            return OutputLines.Skip(scroll).Take(MaxVisibleLines);
        }
    }
}
