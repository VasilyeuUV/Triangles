using System.Drawing;

namespace Triangles.Contracts.Geometry
{
    /// <summary>
    /// Контракт линейной геометрической фигуры
    /// </summary>
    public interface ILinear
    {
        /// <summary>
        /// Цвет линии
        /// </summary>
        Color? LineColor { get; }
    }
}
