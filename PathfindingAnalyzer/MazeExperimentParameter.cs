using System.Collections.Generic;
using Common;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class MazeExperimentParameter
    {
        public Maze Maze{ get; set; }

        // public int NumberOfMazes { get; set; }

        public IList<PathFinder> PathFinders { get; set; }

        public MazeExperimentParameter(Maze maze, IList<PathFinder> pathFinders)
        {
            Maze = maze;
            PathFinders = pathFinders;
        }
    }
}
