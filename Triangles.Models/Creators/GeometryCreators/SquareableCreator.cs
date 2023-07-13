using Triangles.Models.Geometry;
using Triangles.Models.Geometry.Triangles;

namespace Triangles.Models.Creators.GeometryCreators
{
    /// <summary>
    /// Создатель площадных геометрических фигур
    /// </summary>
    public class SquareableCreator : AGeometric2dCreatorBase
    {

        //####################################################################################################
        #region AGeometric2dCreatorBase

        public override ASquareableGeometricFigureBase Create2dFigure(IEnumerable<int> coords)
            => coords.Count() switch
            {
                3 => new TriangleModel(coords),
                _ => throw new InvalidDataException("At the specified coordinates, the squreable figure cannot be built") 
            };


        #endregion // AGeometric2dCreatorBase

    }
}
