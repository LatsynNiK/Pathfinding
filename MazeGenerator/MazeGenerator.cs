using System.Drawing;
using System.Linq;
using Common;

namespace MazeGenerator
{
    public abstract class MazeGenerator
    {
        public virtual Maze Generate(int width, int height)
        {
            return Generate(new MazeGeneratorOptions()
            {
                Width = width,
                Height = height,
                PercentOfWalls = 25,
            });
        }

        public abstract Maze Generate(MazeGeneratorOptions options);

        public virtual void InitStart(Maze maze, MazeGeneratorOptions options)
        {
            switch (options.StartPosition)
            {
                case CheckpointPosition.Center:
                    var suggestedPoints = new[]
                    {
                        new Point(maze.Width/2, maze.Height/2),
                        new Point(maze.Width/2+1, maze.Height/2),
                        new Point(maze.Width/2-1, maze.Height/2),
                        new Point(maze.Width/2, maze.Height/2+1),
                        new Point(maze.Width/2, maze.Height/2-1)
                    };
                    maze.Start = suggestedPoints.First(p => !maze.Field[p.Y, p.X].IsWall);
                    break;
                default:
                    maze.Start = new Point(1, 1);
                    break;
            }
        }

        public virtual void InitFinish(Maze maze, MazeGeneratorOptions options)
        {
            switch (options.FinishPosition)
            {
                case CheckpointPosition.Center:
                    var suggestedPoints = new[]
                    {
                        new Point(maze.Width/2, maze.Height/2),
                        new Point(maze.Width/2+1, maze.Height/2),
                        new Point(maze.Width/2-1, maze.Height/2),
                        new Point(maze.Width/2, maze.Height/2+1),
                        new Point(maze.Width/2, maze.Height/2-1)
                    };
                    maze.Finish = suggestedPoints.First(p => !maze.Field[p.Y, p.X].IsWall);
                    break;
                default:
                    maze.Finish = new Point(maze.Width - 2, maze.Height - 2);
                    break;
            }
        }

    }
}
