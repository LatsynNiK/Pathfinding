using System;
using System.Collections.Generic;
using Services.DiscreteMap;
using Services.Models;

namespace Services.Interfaces
{
    public interface IDiscreteMapService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items">Списки точек, сгруппированых по этажам</param>
        /// <returns>Минимальную точку текущего запроса (радиус-вектор) и максимальную</returns>
        Tuple<DiscreteVector, DiscreteVector> FindMinMaxCoordinate(IList<ItemSm> items);

        void CreateWalls(DiscreteMapField field, IList<ItemSm> items);

        void SetDistanceFromWalls(DiscreteMapField field);

        bool StartWave(DiscreteMapField field, Cell startCell, Cell finishCell);

        IList<Cell> GetBackPath(DiscreteMapField field, Cell startCell, Cell finishCell);
    }
}
