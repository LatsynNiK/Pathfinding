using Common;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class PathfindingExperimentParameter
    {
        public Maze Maze { get; set; }

        public PathFinder PathFinder { get; set; }

        public PathfindingExperimentParameter(Maze maze, PathFinder pathFinder)
        {
            Maze = maze;
            PathFinder = pathFinder;
        }
    }
}
