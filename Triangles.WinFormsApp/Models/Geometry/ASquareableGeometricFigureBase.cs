using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Triangles.Contracts.Geometry;

namespace Triangles.Models.Geometry
{
    public abstract class ASquareableGeometricFigureBase : AGeometricFigure2DBase, ISquareable
    {
        private double _s;                  // - площадь фигуры


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="coords"></param>
        protected ASquareableGeometricFigureBase(IEnumerable<int> coords) : base(coords)
        {
            _s = GetSquare();
        }


        /// <summary>
        /// Уровень вложенности фигуры в другие фигуры
        /// </summary>
        [Range(0, int.MaxValue)]
        public int NestingLevel { get; set; }


        //############################################################################################################
        #region ISquareable

        public double S => _s;

        public Color? FillColor { get; set; }

        public Color? LineColor { get; set; }

        #endregion // ISquareable


        /// <summary>
        /// Метод вычисления площади 2D фигуры
        /// </summary>
        /// <returns></returns>
        protected abstract double GetSquare();


        /// <summary>
        /// Проверка фигуры на коллинеарность 
        /// (когда координаты располагаются на одной линии). 
        /// Площадные фигуры не могут быть коллинеарными
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckCollinear()
        {
            return S == 0;
        }
    }
}
