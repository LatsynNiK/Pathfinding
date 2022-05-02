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

        public IDictionary<PathFinder, long> RunMazeExperiment(MazeExperimentParameter parameter)
        {
            var result = new Dictionary<PathFinder, long>();
            foreach (var pathFinder in parameter.PathFinders)
            {
                var pathfindingExperimentParameter = new PathfindingExperimentParameter(parameter.Maze, pathFinder);
                var pathfinderExperimentResult = RunPathfindingExperiment(pathfindingExperimentParameter);
                result.Add(pathFinder, pathfinderExperimentResult);
            }

            return result;
        }

        long RunPathfindingExperiment(PathfindingExperimentParameter parameter)
        {
            _stopwatch.Start();
            var path = parameter.PathFinder.FindPath(parameter.Maze);
            _stopwatch.Stop();
            var result = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return result;
        }
    }
}
