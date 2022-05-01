using System;
using AStar;
using AStar.Options;
using Common;
namespace Pathfinding
{
    public class AStarPathFinder : PathFinder
    {
        public override Path FindPath(Maze maze)
        {
            var world = ToWorldGrid(maze);
            var pathFinder = new AStar.PathFinder(world, new PathFinderOptions() {UseDiagonals = false, SearchLimit = Int32.MaxValue});
            var trace = pathFinder.FindPath(maze.Start, maze.Finish);
            return new Path(trace);
        }

        private WorldGrid ToWorldGrid(Maze maze)
        {
            var world = new WorldGrid(maze.Height, maze.Width);
            for (int i = 0; i < maze.Height; i++)
            {
                for (int j = 0; j < maze.Width; j++)
                {
                    if (!maze.Field[i, j].IsWall)
                    {
                        world[i, j] = 1;
                    }
                }
            }

            return world;
        }
    }
}
