using System.Drawing;

namespace Pathfinding
{
    public class Path
    {
        public Point[] Trace { get; set; }

        public Path(Point[] trace)
        {
            Trace = trace;
        }
    }
}
