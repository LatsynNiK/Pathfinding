using System;
using System.IO;
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
            //Console.WriteLine("Start analysis 1");
            var result1 = analyzer.RunAnalysis(analysisParameter1);
            
            Console.Write(analysisPrinter.Print(analysisParameter1, result1));
            //Console.WriteLine("Finish analysis 1");
            //Console.WriteLine();
            //Console.WriteLine("Result table");
            //foreach (var pathFinder in result1.PathfindingStatistics.Keys)
            //{
            //    Console.WriteLine($"Pathfinder: {pathFinder.GetType()}");
            //    Console.WriteLine($"\tMaze size\t\tTime (Milliseconds)");
            //    foreach (var line in result1.PathfindingStatistics[pathFinder])
            //    {
            //        Console.WriteLine($"\t{line.Key}\t\t\t{line.Value}");
            //    }
            //}

            
            //textWriter.WriteLine("1111222333;");

            //textWriter.Close();
            //Console.Write(textWriter);


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
            //Console.WriteLine("Start analysis 2");
            var result2 = analyzer.RunAnalysis(analysisParameter2);
            //Console.WriteLine("Finish analysis 2");
            //Console.WriteLine();
            //Console.WriteLine("Result table");
            //foreach (var pathFinder in result2.PathfindingStatistics.Keys)
            //{
            //    Console.WriteLine($"Pathfinder: {pathFinder.GetType()}");
            //    Console.WriteLine($"\tMaze size\t\tTime (Milliseconds)");
            //    foreach (var line in result2.PathfindingStatistics[pathFinder])
            //    {
            //        Console.WriteLine($"\t{line.Key}\t\t\t{line.Value}");
            //    }
            //}

            
            Console.Write(analysisPrinter.Print(analysisParameter2, result2));


            //var mazeGenerator = new EllerMazeGenerator();
            //var maze = mazeGenerator.Generate(new MazeGeneratorOptions()
            //{
            //    Height = 50,
            //    Width = 50,
            //    PercentOfWalls = 25,
            //    IsPerfectMaze = false
            //});
            //var mazePrinter = new MazePrinter();
            //mazePrinter.AddMazeLayer(maze);//.AddStartAndFinish(maze.Start, maze.Finish).Print();

            //var aStarPathFinder = new AStarPathFinder();
            //var aStarPath = aStarPathFinder.FindPath(maze);
            //mazePrinter.ClearPaths().AddPathLayer(aStarPath, "+ ").AddStartAndFinish(maze.Start, maze.Finish).Print();
            //Console.WriteLine(); Console.WriteLine();

            Console.ReadKey();
        }
    }
}
