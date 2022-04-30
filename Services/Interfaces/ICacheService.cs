using System;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(Guid id, Func<T> firstGetFunc) where T : BaseSmModel;
    }
}
