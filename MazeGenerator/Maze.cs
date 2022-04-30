using System.Drawing;

namespace MazeGenerator
{
    public class Maze
    {
        public Cell[,] Field { get; set; }

        public Point Start { get; set; }

        public Point Finish { get; set; }
    }
}
