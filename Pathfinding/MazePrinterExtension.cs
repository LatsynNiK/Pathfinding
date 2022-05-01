using System;
using Common;

namespace Pathfinding
{
    public static class MazePrinterExtension
    {
        public static string Path = "o ";

        public static MazePrinter AddPathLayer(this MazePrinter mazePrinter, Path path, string pathMark = null)
        {
            var pathStr = pathMark ?? Path;
            foreach (var point in path.Trace)
            {
                mazePrinter.PrintedField[point.Y, point.X] = new CellForPrinting(pathStr);
            }
            return mazePrinter;
        }
    }
}
