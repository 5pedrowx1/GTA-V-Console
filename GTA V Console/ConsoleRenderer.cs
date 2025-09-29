using GTA.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Hash = GTA.Native.Hash;

namespace GTA_V_Console
{
    public class ConsoleRenderer
    {
        public List<string> OutputLines { get; private set; } = new List<string>();
        private string statusMessage = "";
        private Color statusColor = Color.White;
        private DateTime statusTime = DateTime.Now;
        private int outputScrollOffset = 0;

        public void ShowMessage(string message, Color color)
        {
            statusMessage = message;
            statusColor = color;
            statusTime = DateTime.Now;
        }

        public void ScrollOutput(int delta)
        {
            outputScrollOffset = Math.Max(0, outputScrollOffset + delta);
            if (outputScrollOffset > Math.Max(0, OutputLines.Count - 20))
                outputScrollOffset = Math.Max(0, OutputLines.Count - 20);
        }

        public void ResetOutputScroll()
        {
            outputScrollOffset = 0;
        }

        public IEnumerable<string> GetVisibleOutputLines(int maxLines)
        {
            return OutputLines.Skip(outputScrollOffset).Take(maxLines);
        }

        public void DrawStatusMessage(Size screenRes, float x, float y)
        {
            if (!string.IsNullOrEmpty(statusMessage) && (DateTime.Now - statusTime).TotalSeconds < 3)
            {
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 0.35f, 0.35f);
                Function.Call(Hash.SET_TEXT_COLOUR, statusColor.R, statusColor.G, statusColor.B, 255);
                Function.Call(Hash.SET_TEXT_OUTLINE);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, statusMessage);
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x / screenRes.Width, y / screenRes.Height);
            }
        }
    }
}
