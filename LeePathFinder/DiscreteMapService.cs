using System.Collections.Generic;
using System.Linq;

namespace LeePathFinder
{
    public class DiscreteMapService
    {
        public bool StartWave(DiscreteMapField field, Cell startCell, Cell finishCell, bool moore = false)
        {
            // Инициализация начального значения волны
            var nextDistance = 0;
            // Дистанция ячейки 
            startCell.Distance = nextDistance;
            var modifiedCells = new List<Cell>(){startCell};
            while (modifiedCells.All(x => x != finishCell) && modifiedCells.Any())
            {
                nextDistance++;
                // Список точек для следующей волны
                var nextCells = modifiedCells.SelectMany(x => field.GetNeighborhood(x, moore).Where(c => c.Available)).Distinct();

                var nextModifiedCells = new List<Cell>();
                foreach (var nextCell in nextCells)
                {
                    if (nextCell.Distance.HasValue && nextCell.Distance.Value <= nextDistance)
                    {
                        continue;
                    }

                    nextCell.Distance = nextDistance;
                    nextModifiedCells.Add(nextCell);
                }
                modifiedCells = nextModifiedCells;
            }
            return finishCell.Distance.HasValue;
        }

        public IList<Cell> GetBackPath(DiscreteMapField field, Cell startCell, Cell finishCell, bool moore = false)
        {
            var result = new List<Cell>();
            var currentCell = finishCell;
            result.Add(currentCell);

            while (currentCell != startCell)
            {
                // Поиск минимальной соседней
                var nextCells = field.GetNeighborhood(currentCell, moore).ToList();
                nextCells = nextCells.Where(x => x.Distance < currentCell.Distance).ToList();
                var nextCell = nextCells.First(x => x.DistanceFromWall == nextCells.Max(c => c.DistanceFromWall));
                result.Add(nextCell);
                currentCell = nextCell;
            }

            result.Reverse();
            return result;
        }
    }
}
