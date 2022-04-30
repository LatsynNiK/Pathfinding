using System;
using System.Collections.Generic;
using Services.Models;

namespace Services.Interfaces
{
    public interface ILevelService
    {
        LevelSm Get(Guid id);
        IList<LevelSm> Get(BuildingSm building);

        LevelSm GetDefault();
    }
}
