using System.Collections.Generic;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class AnalysisParameter
    {
        public IEnumerable<int> MazeSizes { get; set; }
        
        public int NumberOfMazes { get; set; }

        public IList<PathFinder> Pathfinders { get; set; }
    }
}
