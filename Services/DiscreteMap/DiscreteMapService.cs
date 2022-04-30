using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Enums;
using Services.Interfaces;
using Services.Models;

namespace Services.DiscreteMap
{
    public class DiscreteMapService : IDiscreteMapService
    {
        public Tuple<DiscreteVector, DiscreteVector> FindMinMaxCoordinate(IList<ItemSm> items)
        {
            var firstNode = items.First().Nodes.First();

            var minX = firstNode.X;
            var minY = firstNode.Y;
            var maxX = firstNode.X;
            var maxY = firstNode.Y;

            var levels = items.Select(x => x.Level).Distinct().ToList();

            var minL = levels.Select(x => x.Number).Min();
            var maxL = levels.Select(x => x.Number).Max();
            foreach (var node in items.SelectMany(x => x.Nodes))
            {
                if (node.X > maxX)
                    maxX = node.X;
                if (node.Y > maxY)
                    maxY = node.Y;
                if (node.X < minX)
                    minX = node.X;
                if (node.Y < minY)
                    minY = node.Y;
            }

            var minCoordinate = new DiscreteVector(minX, minY, minL);
            var maxCoordinate = new DiscreteVector(maxX, maxY, maxL);

            return new Tuple<DiscreteVector, DiscreteVector>(minCoordinate, maxCoordinate);
        }

        public void CreateWalls(DiscreteMapField field, IList<ItemSm> items)
        {
            foreach (var item in items)
            {
                var node = item.Nodes.First.Value;
                while (node?.NextNode != null)
                {
                    field.AddWall(node, node.NextNode, item.Level.Number);
                    node = node.NextNode;
                }

                // Создание переходов по лестницам
                if (item.TypeItem.Type == ItemType.Stairs)
                {
                    var stairCell = field.GetCell(item.Center, item.Level.Number);
                    stairCell.IsStair = true;
                }
            }
        }

        public void SetDistanceFromWalls(DiscreteMapField field)
        {
            // Начальное значение 
            var nextDistance = 0;
            var nextCells = new List<Cell>();
            foreach (var cell in field.Field)
            {
                // Если ячейка непроходима
                if (!cell.Available)
                {
                    cell.DistanceFromWall = nextDistance;
                    nextCells.Add(cell);
                }
            }

            // Построение волны
            while (nextCells.Any())
            {
                nextDistance++;
                // Список точек для следующей волны
                nextCells = nextCells.SelectMany(x => field.GetNeighborhood(x, true).Where(c => !c.DistanceFromWall.HasValue)).Distinct().ToList();
                foreach (var nextCell in nextCells)
                {
                    nextCell.DistanceFromWall = nextDistance;
                }
            }
        }

        public bool StartWave(DiscreteMapField field, Cell startCell, Cell finishCell)
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
                var nextCells = modifiedCells.SelectMany(x => field.GetNeighborhood(x, true).Where(c => c.Available)).Distinct();

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

        public IList<Cell> GetBackPath(DiscreteMapField field, Cell startCell, Cell finishCell)
        {
            var result = new List<Cell>();
            var currentCell = finishCell;
            result.Add(currentCell);

            while (currentCell != startCell)
            {
                // Поиск минимальной соседней
                var nextCells = field.GetNeighborhood(currentCell, true).ToList();
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
