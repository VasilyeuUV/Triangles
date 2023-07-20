namespace Triangles.Models.Geometry
{
    public abstract class APolygon : ASquareableGeometricFigureBase
    {

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="coords"></param>
        protected APolygon(IEnumerable<int> coords) : base(coords)
        {




        }
    }
}
