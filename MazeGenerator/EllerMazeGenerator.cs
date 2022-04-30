using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class EllerMazeGenerator : MazeGenerator
    {
        public override Maze Generate(int width, int height)
        {
            throw new NotImplementedException();
        }

        private EllerPackage.Maze GetEllerMaze(int width, int height)
        {
            EllerPackage.Maze maze = new EllerPackage.Maze();
            maze.GenerateMaze(width, height);
            return maze;
        }
    }
}
