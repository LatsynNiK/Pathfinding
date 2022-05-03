using Common;

namespace MazeGenerator
{
    public class EllerMazeGenerator : MazeGenerator
    {
        public override Maze Generate(MazeGeneratorOptions options)
        {
            var width = options.Width;
            var height = options.Height;

            var bias = options.IsPerfectMaze ? 50 : (int) options.PercentOfWalls * 2;
            var ellerMaze = GetEllerMaze(ToEllerSize(width), ToEllerSize(height), bias, options.IsPerfectMaze);
            //ellerMaze.PrintMaze();
            var maze = TranslateToMaze(ellerMaze);
            base.InitStart(maze, options);
            base.InitFinish(maze, options);
            return maze;
        }

        private EllerPackage.Maze GetEllerMaze(int width, int height, int bias, bool disableLoops)
        {
            var maze = new EllerPackage.Maze();
            maze.GenerateMaze(width, height, bias, disableLoops);
            //maze.PrintMaze();
            return maze;
        }

        private Maze TranslateToMaze(EllerPackage.Maze ellerMaze)
        {
            var ellerWidth = ellerMaze.Field.GetLength(0);
            var ellerHeight = ellerMaze.Field.GetLength(1);

            var width = ellerWidth * 2 + 1;
            var height = ellerHeight * 2 + 1;
            var resultMaze = new Maze(height, width);
            for (var ie = 0; ie < ellerHeight; ie++)
            {
                var i = 1 + ie*2; 
                for (var je = 0; je < ellerWidth; je++)
                {
                    var j = 1 + je * 2;
                    resultMaze.Field[i, j].IsWall = false;
                    resultMaze.Field[i, j + 1].IsWall = ellerMaze.Field[je, ie].HasRightWall;
                    resultMaze.Field[i + 1, j].IsWall = ellerMaze.Field[je, ie].HasBottomWall;
                    resultMaze.Field[i + 1, j + 1].IsWall = resultMaze.Field[i, j + 1].IsWall || resultMaze.Field[i + 1, j].IsWall;
                }
            }
            // draw left border
            for (var i = 0; i < height; i++)
            {
                resultMaze.Field[i, 0].IsWall = true;
            }
            // draw top border
            for (var j = 0; j < width; j++)
            {
                resultMaze.Field[0, j].IsWall = true;
            }

            return resultMaze;
        }

        private int ToEllerSize(int size)
        {
            var result = (size + 1) / 2;
            return result;
        }
    }
}
