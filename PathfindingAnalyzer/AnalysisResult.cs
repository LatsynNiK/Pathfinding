using System.Collections.Generic;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class AnalysisResult
    {
        // Each Pathfinder has function table: F(x) = Milliseconds, x = MazeSize
        public IDictionary<PathFinder, IDictionary<int, PathfindingExperimentAggregationResult>> PathfindingStatistics { get; set; }

        public AnalysisResult()
        {
            PathfindingStatistics = new Dictionary<PathFinder, IDictionary<int, PathfindingExperimentAggregationResult>>();
        }
    }
}
