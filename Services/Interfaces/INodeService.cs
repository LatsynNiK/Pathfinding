using System;
using System.Collections.Generic;
using Services.Models;

namespace Services.Interfaces
{
    public interface INodeService
    {
        LinkedList<NodeSm> GetLine(Guid id);
    }
}
