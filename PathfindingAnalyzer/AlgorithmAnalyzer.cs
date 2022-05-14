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
            //Console.WriteLine($"Analysis {parameter.Name} started.");
            var result = new AnalysisResult();
            foreach (var pathFinder in parameter.Pathfinders)
            {
                result.PathfindingStatistics.Add(pathFinder, new Dictionary<int, PathfindingExperimentAggregationResult>());
            }

            var mazeGenerator = new EllerMazeGenerator();
            var experimentRunner = new ExperimentRunner();
            foreach (var mazeSize in parameter.MazeSizes)
            {
                Console.WriteLine($"Maze size: {mazeSize}.");
                var mazeSizeStatistics = new Dictionary<PathFinder, IList<PathfindingExperimentResult>>();
                foreach (var pathFinder in parameter.Pathfinders)
                {
                    mazeSizeStatistics.Add(pathFinder, new List<PathfindingExperimentResult>());
                }
                Console.WriteLine($"Processed mazes: 0 / {parameter.NumberOfMazes}");
                var mazeGeneratorOption = new MazeGeneratorOptions()
                {
                    Height = mazeSize,
                    Width = mazeSize,
                    PercentOfWalls = parameter.PercentOfWalls,
                    IsPerfectMaze = parameter.IsPerfectMaze,
                    StartPosition = parameter.StartPosition
                };
                for (int i = 0; i < parameter.NumberOfMazes; i++)
                {
                    var maze = mazeGenerator.Generate(mazeGeneratorOption);
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
                    var aveMilliseconds = mazeSizeStatistic.Value.Select(x => x.Milliseconds).Average();
                    var aveTicks = mazeSizeStatistic.Value.Select(x => x.Ticks).Average();
                    result.PathfindingStatistics[mazeSizeStatistic.Key]
                        .Add(mazeSize, new PathfindingExperimentAggregationResult()
                        {
                            Milliseconds = aveMilliseconds,
                            Ticks = aveTicks
                        });
                }
            }
            Console.WriteLine($"Analysis {parameter.Name} finished.");
            return result;
        }
    }
}
