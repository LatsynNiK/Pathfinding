using System;
using System.Collections.Generic;
using System.Linq;
using MazeGenerator;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class AlgorithmAnalyzer
    {
        public AnalysisResult RunAnalysis(AnalysisParameter parameter)
        {
            var result = new AnalysisResult();
            foreach (var pathFinder in parameter.Pathfinders)
            {
                result.PathfindingStatistics.Add(pathFinder, new Dictionary<int, double>());
            }

            var mazeGenerator = new EllerMazeGenerator();
            var experimentRunner = new ExperimentRunner();
            foreach (var mazeSize in parameter.MazeSizes)
            {
                Console.WriteLine($"Maze size: {mazeSize}.");
                var mazeSizeStatistics = new Dictionary<PathFinder, IList<long>>();
                foreach (var pathFinder in parameter.Pathfinders)
                {
                    mazeSizeStatistics.Add(pathFinder, new List<long>());
                }
                Console.WriteLine($"Processed mazes: 0 / {parameter.NumberOfMazes}");
                for (int i = 0; i < parameter.NumberOfMazes; i++)
                {
                    var maze = mazeGenerator.Generate(mazeSize, mazeSize);
                    var mazeExperimentParameter = new MazeExperimentParameter(maze, parameter.Pathfinders);
                    var mazeExperimentResult = experimentRunner.RunMazeExperiment(mazeExperimentParameter);
                    foreach (var keyValue in mazeExperimentResult)
                    {
                        mazeSizeStatistics[keyValue.Key].Add(keyValue.Value);
                    }

                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.WriteLine($"Processed mazes: {i+1} / {parameter.NumberOfMazes}");
                }
                foreach (var mazeSizeStatistic in mazeSizeStatistics)
                {
                    result.PathfindingStatistics[mazeSizeStatistic.Key].Add(mazeSize, mazeSizeStatistic.Value.Average());
                }
            }

            return result;
        }
    }
}
