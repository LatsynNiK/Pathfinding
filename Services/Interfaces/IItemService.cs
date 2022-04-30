using System;
using System.Collections.Generic;
using Abstractions.Enums;
using Services.Models;

namespace Services.Interfaces
{
    public interface IItemService
    {
        ItemSm Get(Guid id);

        ItemSm Get(string number);

        IList<ItemSm> Get(LevelSm level, ItemType? type = null);

        IList<ItemSm> GetAll();
    }
}
