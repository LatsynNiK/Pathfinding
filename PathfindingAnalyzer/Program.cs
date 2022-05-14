using System;
using System.IO;
using MazeGenerator;
using Pathfinding;

namespace PathfindingAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new AlgorithmAnalyzer();
            var mazeSizes = new[]
            {
                50,
                100,
                200,
                // 500,
                // 1000, 
                //2000, 
                //5000, 
                //10000
            };
            var numberOfMazes = 5;
            
            var analysisPrinter = new AnalysisPrinter();
            
            
            var analysisParameter1 = new AnalysisParameter()
            {
                Name = "First",
                Pathfinders = new PathFinder[] { new Pathfinding.LeePathFinder(), new AStarPathFinder() },
                MazeSizes = mazeSizes,
                NumberOfMazes = numberOfMazes,
                IsPerfectMaze = true,
                // PercentOfWalls = 0
            };
            var result1 = analyzer.RunAnalysis(analysisParameter1);
            Console.Write(analysisPrinter.Print(analysisParameter1, result1));
            
            
            var analysisParameter2 = new AnalysisParameter()
            {
                Name = "Second",
                Pathfinders = new PathFinder[] { new Pathfinding.LeePathFinder(), new AStarPathFinder() },
                MazeSizes = mazeSizes,
                NumberOfMazes = numberOfMazes,
                IsPerfectMaze = false,
                PercentOfWalls = 5,
                StartPosition = CheckpointPosition.Center
            };
            var result2 = analyzer.RunAnalysis(analysisParameter2);
            Console.Write(analysisPrinter.Print(analysisParameter2, result2));



            using (var sw = new StreamWriter("../../../../results.txt"))
            {
                sw.WriteLine(analysisPrinter.Print(analysisParameter1, result1));
                sw.WriteLine(analysisPrinter.Print(analysisParameter2, result2));
            }

            Console.ReadKey();
        }
    }
}
