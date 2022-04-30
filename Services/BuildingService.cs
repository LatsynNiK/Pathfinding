using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class BuildingService : IBuildingService
    {
        private readonly ICacheService _cacheService;

        public BuildingService(ICacheService cacheService, ITypeItemService typeItemService, INodeService nodeService)
        {
            _cacheService = cacheService;
        }

        public BuildingSm Get(Guid id)
        {
            BuildingSm FirstGetFunc()
            {
                using (var db = new NavigatorContext())
                {
                    var building = db.Buildings.Find(id);
                    if (building == null)
                    {
                        return null;
                    }

                    return new BuildingSm()
                    {
                        Id = building.Id,
                        Name = building.Name
                    };
                }
            }

            return _cacheService.Get(id, FirstGetFunc);
        }

        public BuildingSm GetDefault()
        {
            using (var db = new NavigatorContext())
            {
                return Get(db.Buildings.First().Id);
            }            
        }

        public IList<BuildingSm> GetAll()
        {
            using (var db = new NavigatorContext())
            {
                if (db.Buildings.Any())
                {
                    return db.Buildings
                        .AsEnumerable()
                        .Select(x => Get(x.Id))
                        .ToList();
                }
                return null;
            }
        }
    }
}
