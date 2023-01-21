using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OPConsole_0._6
{
    public class Demo
    {
        OPC_Core opc = new OPC_Core();
        Array cols = Enum.GetValues(typeof(ConsoleColor));
        
        //get values from Operations enum as an array of ints
        int[] enumValues = (int[])Enum.GetValues(typeof(Operations));
        Random r = new Random();
        ConsoleColor bgCol, fgCol;
        int maxBox = 5;
        int maxWidth = 80, maxHeight = 25;
        string id = "DemoBox";
        string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public Demo()
        {
            
            GenInfoBox($"This demo will create {maxBox} random boxes, then, everytime you hit enter, OPC will perform a random operation. Demo ends when all boxes have been removed from the canvas. Hit enter when you would like to start performing random operations!");
           
            //create random boxes
            for(int a = 0; a < maxBox; a++)
            {
                GenElement();
            }

            GenInfoBox($"OP Console has generated {maxBox} random boxes with random sizes and colours, and put them in a random position on the canvas. Now, when you hit enter, OP Console will perform a random operation on them...");
            
            while(opc._elements.Count > 0)
            {
                int rand = r.Next(enumValues.Length);

                switch (rand)
                {
                    case 0:
                        //gen element
                        GenElement();
                        GenInfoBox($"A new element has been created! There are now {opc._elements.Count} elements");
                        break;
                    case 1:
                        //resize element
                        ResizeAll();
                        GenInfoBox($"All {opc._elements.Count} elements have been resized!");
                        break;
                    case 2:
                        MoveAllElements();
                        GenInfoBox($"All {opc._elements.Count} elements have been moved!");
                        break;
                    case 3:
                        //change colours
                        ChangeCols();
                        GenInfoBox($"The colours of all {opc._elements.Count} elements have been changed!");
                        break;
                    case 4:
                        //add text
                        TextFill();
                        GenInfoBox($"All {opc._elements.Count} elements have been text filled!");
                        break;
                    case 5:
                        //remove text
                        RemoveElementText();
                        GenInfoBox($"The text has been removed from all {opc._elements.Count} elements!");
                        break;
                    case 6:
                        //delete element
                        DelElement();
                        GenInfoBox($"Element has been deleted! There are now {opc._elements.Count} elements");
                        break;
                    default:
                        break;
                }

            }
            
           
        }

        //creates a randomly sized and coloured element and places it at a random place on the canvas
        private void GenElement()
        {
            int width = r.Next(maxWidth); //max width is window width
            int height = r.Next(maxHeight); //max height is window height
            int top = r.Next(opc.w.GetHeight()); //max pos from top
            int left = r.Next(opc.w.GetWidth()); //max pos from left

            bgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));
            fgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));

            if(bgCol == ConsoleColor.Black || bgCol == fgCol)
            {
                bgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));
                fgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));
            }

            opc.CreateElement(width, height, id, top, left, bgCol, fgCol);
            
        }
        //delete element
        private void DelElement()
        {
            Element getRand = opc._elements[r.Next(opc._elements.Count)];
            opc.DeleteElement(getRand);
        }
        //fill elements with text
        private void TextFill()
        {
            StringBuilder text = new StringBuilder();

            foreach(Element e in opc._elements)
            {
                for(int i = 0; i < e.Cells.Length; i++)
                {
                    text.Append(alphabet[r.Next(alphabet.Length)]);
                }
                opc.AddText(e, text.ToString());
            }
        }
        //remove all text from elements
        private void RemoveElementText()
        {
            foreach(Element e in opc._elements)
            {
                opc.RemoveText(e);
            }
        }
        //move all elements
        private void MoveAllElements()
        {
            foreach(Element e in opc._elements)
            {
                opc.MoveElement(e, r.Next(opc.w.GetHeight()), r.Next(opc.w.GetWidth()));
            }
        }

        //resizes all elements in elements array
        private void ResizeAll()
        {
            //had to make the original List a copy because the loop kept failing since the original list would be edited and it broke the loop
            var eleList = opc._elements.ToList();

            for (int i = 0; i < eleList.Count; i++)
            {
                opc.ResizeElement(eleList[i], r.Next(maxWidth), r.Next(maxHeight));
            }
        }

        //change all colours
        private void ChangeCols()
        {
            
            foreach (Element e in opc._elements)
            {
                ConsoleColor bgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));
                ConsoleColor fgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));

                if (bgCol == ConsoleColor.Black || bgCol == fgCol)
                {
                    bgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));
                    fgCol = (ConsoleColor)cols.GetValue(r.Next(cols.Length));
                }

                opc.ChangeBgCol(e, bgCol);
                opc.ChangeFgCol(e, fgCol);
            }
        }

        private void GenInfoBox(string text)
        {
            int boxWidth = 50;
            int boxHeight = 10;
            int top = (opc.w.GetHeight() / 2) - (boxHeight / 2);
            int left = (opc.w.GetWidth() / 2) - (boxWidth / 2);
            ConsoleColor bgCol = ConsoleColor.Black;
            ConsoleColor fgCol = ConsoleColor.White;
            string id = "MessageBox";

            Element e = opc.CreateElement(boxWidth, boxHeight, id, top, left, bgCol, fgCol);
            opc.AddText(e, text);

            Console.ReadLine();
            opc.DeleteElement(e);
        }

        enum Operations
        {
            CreateElement,
            ResizeElement,
            MoveElement,
            ChangeColours,
            AddText,
            RemoveText,
            DeleteElement
        }
        
    }
}

