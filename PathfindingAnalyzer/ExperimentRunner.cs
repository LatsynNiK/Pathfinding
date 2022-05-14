using System.Collections.Generic;
using System.Diagnostics;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class ExperimentRunner
    {
        private readonly Stopwatch _stopwatch;

        public ExperimentRunner()
        {
            _stopwatch = new Stopwatch();
        }

        public IDictionary<PathFinder, PathfindingExperimentResult> RunMazeExperiment(MazeExperimentParameter parameter)
        {
            var result = new Dictionary<PathFinder, PathfindingExperimentResult>();
            foreach (var pathFinder in parameter.PathFinders)
            {
                var pathfindingExperimentParameter = new PathfindingExperimentParameter(parameter.Maze, pathFinder);
                var pathfinderExperimentResult = RunPathfindingExperiment(pathfindingExperimentParameter);
                result.Add(pathFinder, pathfinderExperimentResult);
            }

            return result;
        }

        PathfindingExperimentResult RunPathfindingExperiment(PathfindingExperimentParameter parameter)
        {
            _stopwatch.Start();
            var path = parameter.PathFinder.FindPath(parameter.Maze);
            _stopwatch.Stop();
            var result = new PathfindingExperimentResult()
            {
                Milliseconds = _stopwatch.ElapsedMilliseconds,
                Ticks = _stopwatch.ElapsedTicks
            };
            _stopwatch.Reset();
            return result;
        }
    }
}
