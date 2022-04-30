using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
    public class ItemSm : BaseSmModel
    {
        public string Description { get; set; }
        public string Number { get; set; }

        public LevelSm Level { get; set; }
        public bool Repair { get; set; }
        
        public TypeItemSm TypeItem { get; set; }

        public LinkedList<NodeSm> Nodes { get; set; }

        public NodeSm Center
        {
            get
            {
                return new NodeSm()
                {
                    X = (Nodes.Select(x => x.X).Max() + Nodes.Select(x => x.X).Min()) / 2,
                    Y = (Nodes.Select(x => x.Y).Max() + Nodes.Select(x => x.Y).Min()) / 2
                };
            }
        }
    }
}