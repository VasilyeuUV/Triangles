namespace Triangles.ViewModels.Windows
{
    /// <summary>
    /// Специальный интерфейс для всех оконных ViewModel
    /// </summary>
    public interface IWindowViewModel
    {
        /// <summary>
        /// Метод об уведомлении о намерении окна закрыться
        /// </summary>
        void WindowClosing();
    }
}
