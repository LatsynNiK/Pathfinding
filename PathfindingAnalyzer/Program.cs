using System;
using Common;
using MazeGenerator;
using Pathfinding;

namespace PathfindingAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new AlgorithmAnalyzer();
            var analysisParameter = new AnalysisParameter()
            {
                Pathfinders = new PathFinder[] {new Pathfinding.LeePathFinder(), new AStarPathFinder()},
                MazeSizes = new[]
                {
                    100,
                    200, 
                    500,
                    //1000, 
                    //2000, 
                    //5000, 
                    //10000
                },
                NumberOfMazes = 100
            };

            Console.WriteLine("Start analysis");
            var result = analyzer.RunAnalysis(analysisParameter);
            Console.WriteLine("Finish analysis");
            Console.WriteLine();
            Console.WriteLine("Result table");
            foreach (var pathFinder in result.PathfindingStatistics.Keys)
            {
                Console.WriteLine($"Pathfinder: {pathFinder.GetType()}");
                foreach (var line in result.PathfindingStatistics[pathFinder])
                {
                    Console.WriteLine($"\tMaze size: {line.Key}\tTime (Milliseconds) {line.Value}");
                }
            }

            Console.ReadKey();
        }
    }
}
