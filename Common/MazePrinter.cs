using System;
using System.Drawing;

namespace Common
{
    public class MazePrinter
    {
        public static string Empty = "  ";
        public static string Wall = "##";

        public static string Start = "S ";
        public static string Finish = "F ";

        public CellForPrinting[,] PrintedField;
        private int _height;
        private int _width;

        public MazePrinter AddMazeLayer(Maze maze)
        {
            _height = maze.Field.GetLength(0);
            _width = maze.Field.GetLength(1);

            PrintedField = new CellForPrinting[_height, _width];

            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    PrintedField[i,j] = new CellForPrinting(maze.Field[i, j].IsWall ? Wall : Empty) ;
                }
            }

            return this;
        }

        public MazePrinter AddStartAndFinish(Point start, Point finish)
        {
            PrintedField[start.Y, start.X] = new CellForPrinting(Start);
            PrintedField[finish.Y, finish.X] = new CellForPrinting(Finish);
            return this;
        }

        public MazePrinter Clear()
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    PrintedField[i, j].Value = Empty;
                }
            }
            return this;
        }

        public MazePrinter ClearPaths()
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    var cell = PrintedField[i, j].Value;
                    if (cell == Wall || cell == Empty) continue;
                    PrintedField[i, j].Value = Empty;
                }
            }
            return this;
        }

        public virtual void Print()
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    Console.Write(PrintedField[i, j].Value);
                }
                Console.WriteLine();
            }
        }
    }

    public struct CellForPrinting
    {
        public string Value { get; set; }

        public CellForPrinting(string value)
        {
            Value = value;
        }
    }
}
