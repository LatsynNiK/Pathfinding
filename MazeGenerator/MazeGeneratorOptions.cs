namespace MazeGenerator
{
    public class MazeGeneratorOptions
    {
        public int Width { get; set; }
        
        public int Height { get; set; }

        public bool IsPerfectMaze { get; set; } = true;

        private int _percentOfWalls = 25;
        public int? PercentOfWalls
        {
            get => IsPerfectMaze ? null : _percentOfWalls;
            set
            {
                if (value != null) _percentOfWalls = value < 50 ? (int)value : 50;
            }
        }
        // Can't be more than 50% (Works for non perfect maze)
        //public int PercentOfWalls { get; set; } = 25;

        public CheckpointPosition StartPosition { get; set; } = CheckpointPosition.TopLeftCorner;

        public CheckpointPosition FinishPosition { get; set; } = CheckpointPosition.Default;
    }

    public enum CheckpointPosition
    {
        Default, // Start - Left-Top corner, Finish - Right-Bottom
        // RandomCorner,
        TopLeftCorner,
        Center,
        // Random
    }
}
