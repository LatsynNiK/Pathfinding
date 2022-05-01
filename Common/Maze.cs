using System.Drawing;

namespace Common
{
    public class Maze
    {
        public Cell[,] Field { get; set; }

        public int Height => Field.GetLength(0);

        public int Width => Field.GetLength(1);

        public Point Start { get; set; }

        public Point Finish { get; set; }

        public Maze(int width, int height)
        {
            Field = new Cell[width, height];
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    Field[i, j] = new Cell();
                }
            }
        }
    }
}
