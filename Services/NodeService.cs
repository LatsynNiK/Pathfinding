using System;
using System.Collections.Generic;
using DataLayer;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class NodeService : INodeService
    {
        public LinkedList<NodeSm> GetLine(Guid id)
        {
            using (var db = new NavigatorContext())
            {
                var list = new LinkedList<NodeSm>();
                var node = db.Nodes.Find(id);
                while (node!= null)
                {
                    var nextSm = new NodeSm { Id = node.Id, X = node.X, Y = node.Y };
                    list.AddLast(nextSm);
                    node = db.Nodes.Find(node.NextNodeId);
                }

                var linkListNodeSm = list.First;
                while (linkListNodeSm?.Next != null)
                {
                    linkListNodeSm.Value.NextNode = linkListNodeSm.Next.Value;
                    linkListNodeSm = linkListNodeSm.Next;
                }

                return list;
            }
        }
    }
}
