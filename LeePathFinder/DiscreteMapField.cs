using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeePathFinder
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
        public int GetLevelNumber(int levelIndex)
        {
            return levelIndex + _radiusVector.Level;
        }

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
