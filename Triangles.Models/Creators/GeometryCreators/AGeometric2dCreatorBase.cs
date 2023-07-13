using Triangles.Models.Geometry;

namespace Triangles.Models.Creators.GeometryCreators
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AGeometric2dCreatorBase
    {
        /// <summary>
        /// Создать геометрическую 2D фигуру
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        abstract public ASquareableGeometricFigureBase Create2dFigure(IEnumerable<int> coords);
    }
}
