using System.Drawing;
using Triangles.Models.Extensions;

namespace Triangles.Models.Geometry
{
    /// <summary>
    /// 2D геометрическая фигура
    /// </summary>
    public abstract class AGeometricFigure2DBase
    {
        private const int _DIMENSION = 2;                                   // - количесвто измерений для 2D


        /// <summary>
        /// Координаты геометрической фигуры на плоскости
        /// </summary>
        public Point[] Coordinates { get; private set; }


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="coords">Значения координат в виде масиива </param>
        protected AGeometricFigure2DBase(IEnumerable<int> coords)
        {
            Coordinates = AddCoordinates(coords).ToArray();
        }


        /// <summary>
        /// Создать и добавить координаты точек
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static IEnumerable<Point> AddCoordinates(IEnumerable<int> coords)
        {
            foreach (var coord in coords.Split(_DIMENSION))
            {
                if (coord.Count() == _DIMENSION)
                    yield return new Point(coord.First(), coord.Last());
            }
        }


        /// <summary>
        /// Валидация фигуры
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckValidation();
    }
}
