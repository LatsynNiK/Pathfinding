using System;
using Common;

namespace Pathfinding
{
    public static class MazePrinterExtension
    {
        public static string Path = "o ";

        public static MazePrinter AddPathLayer(this MazePrinter mazePrinter, Path path)
        {
            foreach (var point in path.Trace)
            {
                mazePrinter.PrintedField[point.Y, point.X] = new CellForPrinting(Path);
            }
            return mazePrinter;
        }
    }
}
