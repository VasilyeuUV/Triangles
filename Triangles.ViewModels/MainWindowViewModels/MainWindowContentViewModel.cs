using Triangles.Models.Geometry.Triangles;

namespace Triangles.ViewModels.MainWindowViewModels
{
    /// <summary>
    /// Вьюмодель содержимого главного окна
    /// </summary>
    public class MainWindowContentViewModel : AViewModelBase
    {
        private IEnumerable<TriangleModel>? _triangles;


        /// <summary>
        /// Список треугольников
        /// </summary>
        public IEnumerable<TriangleModel>? Triangles { get => _triangles; set => _triangles = value; }

    }
}
