using Triangles.ViewModels.Windows.MainWindow.MainWindowContentViewModel;

namespace Triangles.ViewModels.BitmapViewModels
{
    public interface IBitmapViewModel : IMainWindowContentViewModel          // - это контентная вьюмодель
    {
        /// <summary>
        /// Отвечает за инициализацию данных
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
    }
}
