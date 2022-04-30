using System;
using System.Drawing;
using AStar;
using AStar.Options;

namespace MazeGenerator
{
    class Program
    {
        const int Height = 20;
        const int Width = 5;

        static void Main(string[] args)
        {
            var mazeGenerator = new EllerMazeGenerator();
            var maze = mazeGenerator.Generate(5, 5);
            mazeGenerator.Print(maze);
            //EllerPackage.Maze maze = new EllerPackage.Maze();
            //maze.GenerateMaze(Width, Height);

            //maze.PrintMaze();

            //string[,] translatedMaze = maze.TranslateMaze();
            //PrintTranslatedMaze(translatedMaze);

            //var w = (short)0;
            //// var w = short.MaxValue;
            //var matrix = new short [6, 5]
            //{
            //    {w, w, w, w, w },
            //    {w, 7, w, 8, w },
            //    {w, 1, w, 1, w },
            //    {w, 1, w, 1, w },
            //    {w, 1, 1, 1, w },
            //    {w, w, w, w, w },
            //};
            //var world = new WorldGrid(matrix);
            //var pathFinder = new AStar.PathFinder(world, new PathFinderOptions(){UseDiagonals = false});
            //var path = pathFinder.FindPath(new Point(1,1), new Point(3,1));


            Console.ReadKey();
        }

        // Prints a translated maze
        static void PrintTranslatedMaze(string[,] maze)
        {
            for (int i = 0; i < Height * 2 + 2; i++)
            {
                for (int j = 0; j < Width * 2 + 2; j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }           
        }

    }
}
