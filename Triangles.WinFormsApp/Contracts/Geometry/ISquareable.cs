using System.Drawing;

namespace Triangles.Contracts.Geometry
{
    /// <summary>
    /// Контракт объекта, имеющего площадь
    /// </summary>
    public interface ISquareable : ILinear
    {
        /// <summary>
        /// Площадь фигуры
        /// </summary>
        double S { get; }


        /// <summary>
        /// Цвет заливки фигуры
        /// </summary>
        Color? FillColor { get; }

    }
}
