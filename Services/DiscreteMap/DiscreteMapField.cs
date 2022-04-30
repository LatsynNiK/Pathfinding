using System.Collections.Generic;
using System.IO;
using System.Linq;
using Services.Models;

namespace Services.DiscreteMap
{
    public class DiscreteMapField
    {
        private readonly DiscreteVector _radiusVector;

        public Cell[,,] Field { get; set; }

        public DiscreteMapField(DiscreteVector min, DiscreteVector max)
        {
            _radiusVector = min;
            var sizeRadiusVector = max - min + 1;
            Field = new Cell[sizeRadiusVector.X, sizeRadiusVector.Y, sizeRadiusVector.Level];
            for (int l = 0; l < Field.GetLength(2); l++)
            {
                for (int x = 0; x < Field.GetLength(0); x++)
                {
                    for (int y = 0; y < Field.GetLength(1); y++)
                    {
                        Field[x, y, l] = new Cell(new DiscreteVector(x, y, l));
                    }
                }
            }
        }

        public void AddWall(NodeSm startNode, NodeSm finishNode, int levelNumber)
        {
            var startCoordinate = new DiscreteVector(startNode.X, startNode.Y, levelNumber) - _radiusVector;
            var finishCoordinate = new DiscreteVector(finishNode.X, finishNode.Y, levelNumber) - _radiusVector;
            var level = startCoordinate.Level;

            var dx = finishCoordinate.X - startCoordinate.X;
            var dy = finishCoordinate.Y - startCoordinate.Y;

            var x = startCoordinate.X;
            var y = startCoordinate.Y;
            while (x != finishCoordinate.X || y != finishCoordinate.Y)
            {
                Field[x, y, level].Available = false;                

                // Определяем, по какой оси сдвигаться.
                if (finishCoordinate.X - x != 0)
                {
                    x += dx > 0 ? 1 : -1;
                }
                if (finishCoordinate.Y - y != 0)
                {
                    y += dy > 0 ? 1 : -1;
                }
            }
            Field[x, y, level].Available = false;
        }

        public Cell GetCell(DiscreteVector coordinate)
        {
            // Если выпали за границу, то возвращаем null
            if ((coordinate.X < 0 || Field.GetLength(0) <= coordinate.X)
                || (coordinate.Y < 0 || Field.GetLength(1) <= coordinate.Y)
                || (coordinate.Level < 0 || Field.GetLength(2) <= coordinate.Level))
            {
                return null;
            }
            return Field[coordinate.X, coordinate.Y, coordinate.Level];
        }

        public Cell GetCell(NodeSm node, int level)
        {
            var coordinate = new DiscreteVector(node.X, node.Y, level) - _radiusVector;
            return GetCell(coordinate);
        }

        public IEnumerable<Cell> GetNeighborhood(Cell cell, bool moore = false)
        {
            var result = new List<Cell>();

            var currentCoordinate = cell.Coordinate;
            // По часовой стрелке
            var topCell = GetCell(new DiscreteVector(currentCoordinate.X, currentCoordinate.Y + 1, currentCoordinate.Level));
            var rightCell = GetCell(new DiscreteVector(currentCoordinate.X + 1, currentCoordinate.Y, currentCoordinate.Level));
            var bottomCell = GetCell(new DiscreteVector(currentCoordinate.X, currentCoordinate.Y - 1, currentCoordinate.Level));
            var leftCell = GetCell(new DiscreteVector(currentCoordinate.X - 1, currentCoordinate.Y, currentCoordinate.Level));

            result.AddRange(new[] { topCell, rightCell, bottomCell, leftCell });

            if (moore)
            {
                var topRightCell = GetCell(new DiscreteVector(currentCoordinate.X + 1, currentCoordinate.Y + 1, currentCoordinate.Level));
                var bottomRightCell = GetCell(new DiscreteVector(currentCoordinate.X + 1, currentCoordinate.Y - 1, currentCoordinate.Level));
                var bottomLeftCell = GetCell(new DiscreteVector(currentCoordinate.X - 1, currentCoordinate.Y - 1, currentCoordinate.Level));
                var topLeftCell = GetCell(new DiscreteVector(currentCoordinate.X - 1, currentCoordinate.Y + 1, currentCoordinate.Level));
                result.AddRange(new[] { topRightCell, bottomRightCell, bottomLeftCell, topLeftCell });
            }
            

            var underCell = GetCell(new DiscreteVector(currentCoordinate.X, currentCoordinate.Y, currentCoordinate.Level - 1));
            var overCell = GetCell(new DiscreteVector(currentCoordinate.X, currentCoordinate.Y, currentCoordinate.Level + 1));
            
            if (cell.IsStair && (underCell?.IsStair ?? false))
                result.Add(underCell);
            if (cell.IsStair && (overCell?.IsStair ?? false))
                result.Add(overCell);

            return result.Where(x => x != null);
        }

        public IEnumerable<NodeSm> GetNodes(IEnumerable<Cell> cells)
        {
            var result = cells.Select(x =>
            {
                var resultVector = x.Coordinate + _radiusVector;
                return new NodeSm()
                {
                    X = resultVector.X,
                    Y = resultVector.Y,
                    //Id = Guid.NewGuid()
                };
            }).ToArray();

            for (int i = 0; i < result.Length-1; i++)
            {
                result[i].NextNode = result[i + 1];
            }

            return result;
        }

        public int GetLevelNumber(int levelIndex)
        {
            return levelIndex + _radiusVector.Level;
        }

        //public IEnumerable<Cell> GetAdjacentCellsNew(Cell cell)
        //{
        //    var result = new List<Cell>();

        //    var currentCoordinate = new DiscreteVector(cell.Coordinate);
        //    // По часовой стрелке
        //    currentCoordinate.Y += 2;
        //    var coord1 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X++;
        //    var coord2 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X++; currentCoordinate.Y--;
        //    var coord3 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X++; currentCoordinate.Y--;
        //    var coord4 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.Y--;
        //    var coord5 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.Y--;
        //    var coord6 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X--; currentCoordinate.Y--;
        //    var coord7 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X--; currentCoordinate.Y--;
        //    var coord8 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X--;
        //    var coord9 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X--;
        //    var coord10 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X--; currentCoordinate.Y++;
        //    var coord11 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X--; currentCoordinate.Y++;
        //    var coord12 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.Y++;
        //    var coord13 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.Y++;
        //    var coord14 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X++; currentCoordinate.Y++;
        //    var coord15 = new DiscreteVector(currentCoordinate);
        //    currentCoordinate.X++; currentCoordinate.Y++;
        //    var coord16 = new DiscreteVector(currentCoordinate);

        //    var ringArray = new[]
        //    {
        //        coord1,
        //        coord2,
        //        coord3,
        //        coord4,
        //        coord5,
        //        coord6,
        //        coord7,
        //        coord8,
        //        coord9,
        //        coord10,
        //        coord11,
        //        coord12,
        //        coord13,
        //        coord14,
        //        coord15,
        //        coord16,
        //    };

        //    result.AddRange(ringArray.Select(GetCell));

        //    var underCell = GetCell(new DiscreteVector(cell.Coordinate.X, cell.Coordinate.Y, cell.Coordinate.Level - 1));
        //    var overCell = GetCell(new DiscreteVector(cell.Coordinate.X, cell.Coordinate.Y, cell.Coordinate.Level + 1));

        //    if (cell.IsStair && (underCell?.IsStair ?? false))
        //        result.Add(underCell);
        //    if (cell.IsStair && (overCell?.IsStair ?? false))
        //        result.Add(overCell);

        //    return result.Where(x => x != null);
        //}

        public void PrintToFile(IEnumerable<Cell> path = null)
        {
            var pathHashSet = path != null
                ? new HashSet<Cell>(path)
                : new HashSet<Cell>();
            using (var sw = new StreamWriter(@"C:\Navigator\field.txt", append: false))
            {
                // levels
                for (int l = 0; l < Field.GetLength(2); l++)
                {
                    sw.WriteLine();
                    sw.WriteLine($"Level {l}");
                    sw.WriteLine();
                    for (int x = 0; x < Field.GetLength(0); x++)
                    {
                        for (int y = 0; y < Field.GetLength(1); y++)
                        {
                            var cell = Field[x, y, l];
                            if (!cell.Available)
                            {
                                sw.Write('#');
                                continue;
                            }                            
                            if (!cell.Distance.HasValue)
                            {
                                sw.Write('.');
                                continue;
                            }

                            if (pathHashSet.Contains(cell))
                            {
                                sw.Write('X');
                                continue;
                            }
                            var distanceStr = cell.Distance.ToString();
                            //var ch = distanceStr.Length == 1 ? distanceStr[0] : distanceStr[distanceStr.Length-2];
                            var ch = distanceStr.Last();
                            sw.Write(ch);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
