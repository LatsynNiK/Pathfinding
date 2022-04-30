using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Enums;
using Abstractions.Interfaces;
using Abstractions.ViewModels;
using Services.DiscreteMap;
using Services.Interfaces;

namespace Services.OuterServices
{
    public class MainMapService : IMainMapService
    {
        private readonly ILevelService _levelService;
        private readonly IBuildingService _buildingService;
        private readonly IItemService _itemService;
        private readonly IDiscreteMapService _discreteMapService;

        public MainMapService(ILevelService levelService, IBuildingService buildingService, IItemService itemService, IDiscreteMapService discreteMapService)
        {
            _levelService = levelService;
            _buildingService = buildingService;
            _itemService = itemService;
            _discreteMapService = discreteMapService;
        }

        public MainMapVm GetMainMap(Guid? levelId = null)
        {
            var currentLevel = levelId.HasValue
                ? _levelService.Get(levelId.Value)
                : _levelService.GetDefault();

            var levels = _levelService.Get(currentLevel.Building)
                .Select(x => new LevelVm()
                {
                    Id = x.Id,
                    Number = x.Number
                })
                .OrderBy(x => x.Number);

            var border = _itemService.Get(currentLevel, ItemType.Border)
                .First(x => x.TypeItem.Type == ItemType.Border)
                .Nodes.Select(n => new PointVm(n.X, n.Y));

            var rooms = _itemService.Get(currentLevel, ItemType.Room)
                .Select(x => new ItemVm()
                {
                    ItemId = x.Id,
                    Number = x.Number,
                    Description = x.Description,
                    Border = x.Nodes.Select(n => new PointVm(n.X, n.Y))
                });

            var stairs = _itemService.Get(currentLevel, ItemType.Stairs)
                .Select(x => new ItemVm()
                {
                    ItemId = x.Id,
                    Border = x.Nodes.Select(n => new PointVm(n.X, n.Y))
                });

            var walls = _itemService.Get(currentLevel, ItemType.Wall)
                .Select(x => new ItemVm()
                {
                    ItemId = x.Id,
                    Border = x.Nodes.Select(n => new PointVm(n.X, n.Y))
                });
            
            var wcWomans = _itemService.Get(currentLevel, ItemType.WсWoman)
                .Select(x => new ItemVm()
                {
                    ItemId = x.Id,
                    Border = x.Nodes.Select(n => new PointVm(n.X, n.Y))
                });

            var wcMans = _itemService.Get(currentLevel, ItemType.WcMan)
                .Select(x => new ItemVm()
                {
                    ItemId = x.Id,
                    Border = x.Nodes.Select(n => new PointVm(n.X, n.Y))
                });

            var buildings = _buildingService.GetAll()
                .Select(x => new BuildingVm()
                {
                    Name = x.Name,
                    FirstLevelId = _levelService.Get(x)
                                       .FirstOrDefault(l => l.Number == 1)?.Id
                                   ?? throw new ApplicationException("Этаж не найден!")
                });

            var items = _itemService.GetAll()
                .Select(x => new ItemVm()
                {
                    ItemId = x.Id,
                    Number = x.Number
                })
                .Distinct()
                .OrderBy(x => x.Number);

            var model = new MainMapVm
            {
                CurrentLevelId = currentLevel.Id,
                Border = border,
                Rooms = rooms,
                Stairs = stairs,
                AllLevels = levels,
                AllBuildings = buildings,
                AllItems = items,
                Walls = walls,
                WcMans = wcMans,
                WcWomans = wcWomans
            };
            return model;
        }

        public PathVm GetPath(string startItemStr, string finishItemStr)
        {
            // Получение id введенных объектов
            var startItem = _itemService.Get(startItemStr);
            var finishItem = _itemService.Get(finishItemStr);

            // Этажи начала и окончания поиска
            var startLevel = startItem.Level;
            var finishLevel = finishItem.Level;

            // Если объкты находятся не в одном здании, то вывести исключение
            if (startLevel.Building != finishLevel.Building)
            {
                throw new ApplicationException();
            }
            var building = startLevel.Building;

            // Минимальное и максимальное значение этажей
            var minLevelNumber = new[] { startLevel, finishLevel }.Select(x => x.Number).Min();
            var maxLevelNumber = new[] { startLevel, finishLevel }.Select(x => x.Number).Max();

            // Получение всех этажей, находящихся между минимальным и максмальным этажами
            var filteredLevels = _levelService.Get(building)
                .Where(x => x.Number >= minLevelNumber && x.Number <= maxLevelNumber)
                .OrderBy(x => x.Number)
                .ToDictionary(x => x.Number);

            // Все объекты находящиеся на этажах
            var items = filteredLevels.Values.SelectMany(x => _itemService.Get(x)).ToList();

            // Точки начала и окончания волны
            var minMax = _discreteMapService.FindMinMaxCoordinate(items);

            // Построение карты и волны
            var field = new DiscreteMapField(minMax.Item1, minMax.Item2);
            _discreteMapService.CreateWalls(field, items);
            _discreteMapService.SetDistanceFromWalls(field);
            var start = field.GetCell(startItem.Center, startItem.Level.Number);
            var finish = field.GetCell(finishItem.Center, finishItem.Level.Number);
            var success = _discreteMapService.StartWave(field, start, finish);
            
            // Если волна дошла до точки конца, то собрать его
            var path = success
                ? _discreteMapService.GetBackPath(field, start, finish)
                : null;

            path = GetKeyCells(path);

            // Т к поле принимало на вход NodeSm, то и возвращяем на выходе их же
            var pathForLevel = path.GroupBy(x => x.Coordinate.Level)
                .Select(grp =>
                {
                    var levelSm = filteredLevels[field.GetLevelNumber(grp.Key)];
                    return new PathForLevelVm()
                    {
                        Level = new LevelVm()
                        {
                            Id = levelSm.Id,
                            Number = levelSm.Number
                        },
                        Path = field.GetNodes(grp)
                            .Select(n => new PointVm(n.X, n.Y))
                            .ToList()
                    };
                });

            return new PathVm()
            {
                FullPath = pathForLevel.ToList()
            };
        }

        private IList<Cell> GetKeyCells(IList<Cell> pathCells)
        {
            var keyCells = new List<Cell>();
            // Установка первой точки
            var lastCell = pathCells.FirstOrDefault();

            // DisplacementVector - Вектор перемещения за один шаг.
            // Если он изменился, то это означает поворот пути и точка поворота должна быть сохранена
            var lastDisplacementVector = new DiscreteVector(0, 0, 0);
            foreach (var cell in pathCells)
            {
                var newDisplacementVector = cell.Coordinate - lastCell?.Coordinate;
                if (!lastDisplacementVector.Equals(newDisplacementVector))
                {
                    keyCells.Add(lastCell);
                }

                lastCell = cell;
                lastDisplacementVector = newDisplacementVector;
            }

            return keyCells;
        }
    }
}
