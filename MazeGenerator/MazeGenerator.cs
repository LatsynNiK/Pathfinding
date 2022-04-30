using System;

namespace MazeGenerator
{
    public abstract class MazeGenerator
    {
        private const char Path = ' ';
        private const char Wall = '#';

        public abstract Maze Generate(int width, int height);

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
