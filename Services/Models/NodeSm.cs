namespace Services.Models
{
    public class NodeSm : BaseSmModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public NodeSm NextNode { get; set; }
    }
}
