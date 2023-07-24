using Triangles.Models.Geometry.Triangles;

namespace Triangles.WinFormsApp.Services.GeometryServices
{


    internal static class GeometryService
    {

        //####################################################################################################
        #region IGeometryService

        public static IEnumerable<TriangleModel> GetTriangles(IEnumerable<int[]> coordinates)
        {

            List<TriangleModel> triangles = new List<TriangleModel>();
            foreach (var coord in coordinates)
            {
                var triangle = new TriangleModel(coord);
                triangles.Add(triangle);
            }
            return triangles.OrderByDescending(t => t.S);
        }

        #endregion // IGeometryService

    }
}
