using System.Drawing;
using Common;

namespace MazeGenerator
{
    public abstract class MazeGenerator
    {
        public abstract Maze Generate(int width, int height);

        public virtual void InitStartAndFinish(Maze maze)
        {
            maze.Start = new Point(1, 1);
            maze.Finish = new Point(maze.Width - 2, maze.Height- 2);
        }
    }
}
