using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPConsole_0._6
{
    public class Cell
    {
        public string ID;
        public int internalID { get; set; }
        public char Char;
        public int TopPos;//position of Cell in Element
        public int LeftPos;
        public int TopCanvasPos;
        public int LeftCanvasPos;
        public ConsoleColor bgColour;
        public ConsoleColor fgColour;
        public int zIndex;

        public Cell(string id, char c, ConsoleColor bg, ConsoleColor fg, int leftPos, int topPos, int internalId)
        {
            this.ID = id;
            this.Char = c;
            this.bgColour = bg;
            this.fgColour = fg;
            this.LeftPos = leftPos;
            this.TopPos = topPos;
            this.internalID = internalId;
        }
    }
}
