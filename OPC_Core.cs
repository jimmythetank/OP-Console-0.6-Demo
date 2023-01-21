using OPConsolev0._6;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace OPConsole_0._6
{
    public class OPC_Core
    {
        //the canvas is a 2d List of Lists that store Cells
        public List<Cell>[,] _canvas;
        //for storing Elements
        public List<Element> _elements = new List<Element>();
        //instantiate the Window class
        public Window w = new Window();
        //eID is used to generate an internal ID of each element
        private int eID = 0;
        
        //default constructor
        public OPC_Core()
        {
            
            //instantiate canvas - this instantiates a canvas List that stores Lists of Cells
            _canvas = new List<Cell>[w.GetHeight(), w.GetWidth()];
            
            for(int i = 0; i < _canvas.GetLength(0); i++)
             {               
                for(int j = 0; j < _canvas.GetLength(1); j++)
                {
                    //add List to every index of canvas and create a default Cell
                    _canvas[i, j] = new List<Cell>
                    {
                        new Cell("Canvas", ' ', ConsoleColor.Black, ConsoleColor.Black, i, j, 0)
                    };
                    //set the zIndex of each Cell to 0
                    _canvas[i, j][0].zIndex = 0;
                    
                    //set canvasPositions of Cells (otherwise DrawCell wont work
                    _canvas[i, j][0].LeftCanvasPos = j;
                    _canvas[i, j][0].TopCanvasPos = i;
                    //draw each Cell to console window
                    DrawCell(_canvas[i, j][0]);
                }
             }
        }
        //draws a single cell to console window
        public void DrawCell(Cell c)
        {
            Console.CursorLeft = c.LeftCanvasPos;
            Console.CursorTop = c.TopCanvasPos;
            Console.BackgroundColor = c.bgColour;
            Console.ForegroundColor = c.fgColour;
            Console.Write(c.Char);
            Console.ResetColor();
        }

        //delete a single Cell from canvas


        //returns an Element for your ui
        public Element CreateElement(int width, int height, string id, int top, int left, ConsoleColor bgCol, ConsoleColor fgCol)
        {
            eID++;
            Element e = new Element(width, height, id, top, left, bgCol, fgCol, eID);

            _elements.Add(e);

            PlaceElement(e);

            return e;
        }

        //place the element into the canvas at given coords
        public void PlaceElement(Element e)
        {
            int canvasHeight = _canvas.GetLength(0);
            int canvasWidth = _canvas.GetLength(1);

            //check the size and position of the element and make sure it does not go off the canvas
            if(e.Width + e.Left > canvasWidth)
            {
                e.Left = canvasWidth - e.Width;
                e.ResetCellCanvasPosition();
            }
            
            if(e.Height + e.Top > canvasHeight)
            {
                e.Top = canvasHeight - e.Height;
                e.ResetCellCanvasPosition();
            }
            
            //draw Cells to canvas
            foreach(Cell c in e.Cells)
            {
                //add cell to canvas at correct coords
                _canvas[c.TopCanvasPos, c.LeftCanvasPos].Add(c);
                //draw cell onto canvas
                DrawCell(c);  
            }
            
        }

        public void MoveElement(Element e, int top, int left)
        {
            e.Top = top;
            e.Left = left;

            //remove old cells from canvas
            foreach (Cell c in e.Cells)
            {
                _canvas[c.TopCanvasPos, c.LeftCanvasPos].Remove(c);
                //draw the topmost Cell from canvas array to console window
                DrawCell(_canvas[c.TopCanvasPos, c.LeftCanvasPos].Last());
            }

            e.ResetCellCanvasPosition();

            PlaceElement(e);
        }
        

        //resize your element
        public void ResizeElement(Element e, int width, int height)
        {
            //get all variable values from old element
            int top = e.Top;
            int left = e.Left;
            ConsoleColor bgColour = e.BgColour;
            ConsoleColor fgColour = e.FgColour;
            string textString = e.TextString;
            string id = e.ID;

            Element e1 = CreateElement(width, height, id, top, left, bgColour, fgColour);
            e1.TextString = textString;

            if (e1.TextString != null)
            {
                AddText(e1, e1.TextString);
            }

            DeleteElement(e);
                 
        }
        //change colours
        public void ChangeBgCol(Element e, ConsoleColor c)
        {
            e.BgColour = c;

            foreach (Cell cell in e.Cells)
            {
                cell.bgColour = c;
                DrawCell(cell);
            }
        }
        public void ChangeFgCol(Element e, ConsoleColor c)
        {
            e.FgColour = c;

            foreach (Cell cell in e.Cells)
            {
                cell.fgColour = c;
                DrawCell(cell);
            }
        }
        //delete an element from the canvas
        public void DeleteElement(Element e)
        {
            foreach(Cell c in e.Cells)
            {
                _canvas[c.TopCanvasPos, c.LeftCanvasPos].Remove(c);
                //draw the topmost Cell from canvas array to console window
                DrawCell(_canvas[c.TopCanvasPos, c.LeftCanvasPos].Last());
            }
            
            _elements.Remove(e);
            
        }

        //TEXT STUFF
        //add text to your element
        public void AddText(Element e, string text)
        {
            
            //the index of the text array
            int index = 0;

            e.TextString = text;

            //add all chars from text to Cells from Cell array
            foreach (Cell c in e.Cells)
            {
                if (index >= text.Length || index >= e.Cells.Length)
                {
                    break;
                }

                c.Char = text[index];
                index++;

                DrawCell(c);
            }

        }
        //remove all text from element
        public void RemoveText(Element e)
        {
            foreach (Cell c in e.Cells)
            {
                c.Char = ' ';
                DrawCell(c);
            }

            e.TextString = null;
        }

    }
}
