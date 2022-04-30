using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class LevelService : ILevelService
    {
        private readonly IBuildingService _buildingService;
        private readonly ICacheService _cacheService;

        public LevelService(ICacheService cacheService, IBuildingService buildingService)
        {
            _buildingService = buildingService;
            _cacheService = cacheService;
        }

        public LevelSm Get(Guid id)
        {
            LevelSm FirstGetFunc()
            {
                using (var db = new NavigatorContext())
                {
                    var level = db.Levels.Find(id);
                    if (level == null)
                    {
                        return null;
                    }

                    return new LevelSm()
                    {
                        Id = level.Id,
                        Number = level.Number,
                        Building = _buildingService.Get(level.BuildingId),
                    };
                }
            }

            return _cacheService.Get(id, FirstGetFunc);
        }

        public IList<LevelSm> Get(BuildingSm building)
        {
            using (var db = new NavigatorContext())
            {
                return db.Levels
                    .Where(x => x.BuildingId == building.Id)
                    .AsEnumerable()
                    .Select(level => Get(level.Id))
                    .ToList();
            }
        }

        public LevelSm GetDefault()
        {
            var defaultBuilding = _buildingService.GetDefault();
            return Get(defaultBuilding).First(x => x.Number == 1);
        }
    }
}
