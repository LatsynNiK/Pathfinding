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
            
            Console.WriteLine("Initial maze");
            Console.WriteLine();
            mazePrinter.AddMazeLayer(maze).AddStartAndFinish(maze.Start, maze.Finish).Print();
            Console.WriteLine(); Console.WriteLine();

            Console.WriteLine("Lee Path");
            Console.WriteLine();
            var leePathFinder = new LeePathFinder();
            var leePath = leePathFinder.FindPath(maze);
            mazePrinter.AddPathLayer(leePath).AddStartAndFinish(maze.Start, maze.Finish).Print();
            Console.WriteLine(); Console.WriteLine();

            Console.WriteLine("A Star Path");
            Console.WriteLine();
            var aStarPathFinder = new AStarPathFinder();
            var aStarPath = aStarPathFinder.FindPath(maze);
            mazePrinter.ClearPaths().AddPathLayer(aStarPath, "+ ").AddStartAndFinish(maze.Start, maze.Finish).Print();
            Console.WriteLine(); Console.WriteLine();

            Console.ReadKey();
        }
    }
}
