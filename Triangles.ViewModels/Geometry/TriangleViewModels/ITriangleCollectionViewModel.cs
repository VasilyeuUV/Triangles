using Triangles.ViewModels.Windows.MainWindow.MainWindowContentViewModel;

namespace Triangles.ViewModels.Geometry.TriangleViewModels
{
    /// <summary>
    /// Интерфейс для регистрации в контейнере коллекции треугольников
    /// </summary>
    public interface ITriangleCollectionViewModel : IMainWindowContentViewModel          // - это контентная вьюмодель
    {
        /// <summary>
        /// Отвечает за инициализацию данных
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
    }
}
