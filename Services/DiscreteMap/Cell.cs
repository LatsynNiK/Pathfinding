namespace Services.DiscreteMap
{
    public class Cell
    {
        public DiscreteVector Coordinate { get; set; }

        public bool Available { get; set; }

        public bool IsStair { get; set; }

        public int? Distance { get; set; }

        public int? DistanceFromWall { get; set; }

        public Cell(DiscreteVector coordinate)
        {
            Coordinate = coordinate;
            Available = true;
        }
    }
}
