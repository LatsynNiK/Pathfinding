using System;
using Abstractions.Enums;
using DataLayer;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class TypeItemService : ITypeItemService
    {
        public TypeItemSm Get(Guid id)
        {
            using (var db = new NavigatorContext())
            {
                var typeItem = db.TypeItems.Find(id);
                if (typeItem == null)
                {
                    return null;
                }

                return new TypeItemSm()
                {
                    Id = typeItem.Id,
                    Name = typeItem.Name,
                    Type = (ItemType)typeItem.Code,
                };
            }
        }
    }
}
