using System;
using Common;
using MazeGenerator;

namespace Pathfinding
{
    class Program
    {
        static void Main(string[] args)
        {
            var mazeGenerator = new EllerMazeGenerator();
            var maze = mazeGenerator.Generate(50, 50);
            var mazePrinter = new MazePrinter();
            //mazePrinter.AddMazeLayer(maze).Print();
            //Console.WriteLine();
            //Console.WriteLine();
            //mazePrinter.AddStartAndFinish(maze.Start, maze.Finish).Print();
            //Console.WriteLine();
            //Console.WriteLine();
            var leePathFinder = new LeePathFinder();
            var path = leePathFinder.FindPath(maze);
            mazePrinter.AddMazeLayer(maze).AddPathLayer(path).AddStartAndFinish(maze.Start, maze.Finish).Print();
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
