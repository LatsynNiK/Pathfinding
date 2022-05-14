using System.Collections.Generic;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class MazeExperimentResult
    {
        public MazeExperimentResult()
        {
            Statistics = new Dictionary<PathFinder, PathfindingExperimentResult>(); ;
        }

        public IDictionary<PathFinder, PathfindingExperimentResult> Statistics { get; set; }
    }
}
