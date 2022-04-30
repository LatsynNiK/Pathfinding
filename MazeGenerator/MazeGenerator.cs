using System;
using System.Drawing;

namespace MazeGenerator
{
    public abstract class MazeGenerator
    {
        private static string Path = "  ";
        private static string Wall = "##";

        public abstract Maze Generate(int width, int height);

        public virtual void InitStartAndFinish(Maze maze)
        {
            maze.Start = new Point(1, 1);
            maze.Start = new Point(maze.Height - 2, maze.Width - 2);
        }

        public void Print(Maze maze)
        {
            var height = maze.Field.GetLength(0);
            var width = maze.Field.GetLength(1);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Console.Write(maze.Field[i, j].IsWall ? Wall : Path);
                }
                Console.WriteLine();
            }
        }
    }
}
