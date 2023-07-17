namespace Triangles.ViewModels.Windows
{
    /// <summary>
    /// Интерфейс менеджера окон
    /// </summary>
    public interface IWindowManager
    {
        /// <summary>
        /// Создать и показать окно
        /// </summary>
        /// <typeparam name="TWindowViewModel"></typeparam>
        /// <param name="viewModel">Вьюмодель окна</param>
        /// <returns>Окно, соответствующее вьюмодели</returns>
        IWindow Show<TWindowViewModel>(TWindowViewModel viewModel)
            where TWindowViewModel : IWindowViewModel;


        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <typeparam name="TWindowViewModel"></typeparam>
        /// <param name="viewModel">Вьюмодель окна</param>
        void Close<TWindowViewModel>(TWindowViewModel viewModel)
            where TWindowViewModel : IWindowViewModel;

    }
}
