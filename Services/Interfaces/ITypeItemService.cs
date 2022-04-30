using System;
using Services.Models;

namespace Services.Interfaces
{
    public interface ITypeItemService
    {
        TypeItemSm Get(Guid id);
    }
}
