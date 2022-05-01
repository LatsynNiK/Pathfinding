using System.Drawing;
using System.Linq;
using Common;
using LeePathFinder;
using MazeGenerator;

namespace Pathfinding
{
    public class LeePathFinder : PathFinder
    {
        public override Path FindPath(Maze maze)
        {
            var field = ToDiscreteMapField(maze);
            var service = new DiscreteMapService();
            var startVector = new DiscreteVector(maze.Start.X, maze.Start.Y);
            var startCell = field.GetCell(startVector);

            var finishVector = new DiscreteVector(maze.Finish.X, maze.Finish.Y);
            var finishCell = field.GetCell(finishVector);

            service.StartWave(field, startCell, finishCell);
            var pathCells = service.GetBackPath(field, startCell, finishCell);
            var path = new Path(pathCells.Select(p => new Point(p.Coordinate.X, p.Coordinate.Y)).ToArray());
            return path;
        }

        private DiscreteMapField ToDiscreteMapField(Maze maze)
        {
            var minDiscreteVector = new DiscreteVector();
            var maxDiscreteVector = new DiscreteVector(maze.Width -1 , maze.Height - 1);
            var discreteMapField = new DiscreteMapField(minDiscreteVector, maxDiscreteVector);
            AddWalls(discreteMapField, maze);
            return discreteMapField;
        }

        private void AddWalls(DiscreteMapField field, Maze maze)
        {
            for (int i = 0; i < maze.Height; i++)
            {
                for (int j = 0; j < maze.Width; j++)
                {
                    field.Field[j, i, 0].Available = !maze.Field[i, j].IsWall;
                }
            }
        }
    }
}
