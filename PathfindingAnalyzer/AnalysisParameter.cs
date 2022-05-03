using System.Collections.Generic;
using MazeGenerator;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class AnalysisParameter
    {
        public string Name { get; set; }

        public IEnumerable<int> MazeSizes { get; set; }
        
        public int NumberOfMazes { get; set; }

        public IList<PathFinder> Pathfinders { get; set; }

        public bool IsPerfectMaze { get; set; }

        private int _percentOfWalls;
        public int? PercentOfWalls
        {
            get => IsPerfectMaze ? null : _percentOfWalls;
            set
            {
                if (value != null) _percentOfWalls = value < 50 ? (int) value : 50;
            }
        }

        public CheckpointPosition StartPosition { get; set; }
    }
}
