using System.Diagnostics;

namespace PathfindingAnalyzer
{
    public class ExperimentRunner
    {
        private readonly Stopwatch _stopwatch;

        public ExperimentRunner()
        {
            _stopwatch = new Stopwatch();
        }

        public MazeExperimentResult RunMazeExperiment(MazeExperimentParameter parameter)
        {
            var result = new MazeExperimentResult();
            foreach (var pathFinder in parameter.PathFinders)
            {
                var pathfindingExperimentParameter = new PathfindingExperimentParameter(parameter.Maze, pathFinder);
                var pathfinderExperimentResult = RunPathfindingExperiment(pathfindingExperimentParameter);
                result.Statistics.Add(pathFinder, pathfinderExperimentResult);
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
