using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPConsole_0._6
{
    public class Element
    {
        //an array of the cells within the Element
        public Cell[,] Cells { get; set; }
        
        //dimensions and position of the element on the canvas
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public ConsoleColor BgColour { get; set; }  
        public ConsoleColor FgColour { get; set; }
        public string TextString { get; set; }
        public string ID { get; set; }
        //internalID used to differenciate Elements from one another, ID can be duplicated
        public int internalID { get; set; }

        public Element(int width, int height, string id, int top, int left, ConsoleColor bgCol, ConsoleColor fgCol, int eID)
        {
            this.Width = width;
            this.Height = height;
            this.ID = id;
            this.Top = top;
            this.Left = left;
            this.BgColour = bgCol;
            this.FgColour = fgCol;
            this.internalID = eID;

            //instantiate Cell array at the dimensions matching the Element
            this.Cells = new Cell[Height, Width];

            //fill array with empty/default Cells
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    //makes Cells and inputs the coords
                    this.Cells[i, j] = new Cell(ID, ' ', BgColour, FgColour, i, j, internalID);
                    this.Cells[i, j].TopCanvasPos = i + Top;
                    this.Cells[i, j].LeftCanvasPos = j + Left;
                }
            }
        }
        //setup the cell array
        public void ResetCellCanvasPosition()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    //input new Cell canvas coords (used by PlaceElement incase the inputted coords would be out of bounds)
                    this.Cells[i, j].TopCanvasPos = i + Top;
                    this.Cells[i, j].LeftCanvasPos = j + Left;
                }
            }
        }

        
    }
}
