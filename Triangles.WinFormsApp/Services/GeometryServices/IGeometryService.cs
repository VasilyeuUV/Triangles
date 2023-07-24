using Triangles.Models.Geometry.Triangles;

namespace Triangles.WinFormsApp.Services.GeometryServices
{
    public interface IGeometryService
    {
        /// <summary>
        /// Получить список треугольников по заданным координатам
        /// </summary>
        /// <param name="coordinates">Список координат</param>
        /// <returns></returns>
        IEnumerable<TriangleModel> GetTriangles(IEnumerable<int[]> coordinates);
    }
}
