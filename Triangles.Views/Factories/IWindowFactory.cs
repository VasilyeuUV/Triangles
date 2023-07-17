using Triangles.ViewModels.Windows;

namespace Triangles.Views.Factories
{
    /// <summary>
    /// Интерфейс фабрики создания окон
    /// </summary>
    public interface IWindowFactory
    {
        /// <summary>
        /// Создать окно
        /// </summary>
        /// <typeparam name="TWindowViewModel">Generic для построения карты соответствия вьюмоделей</typeparam>
        /// <param name="viewModel">Вьюмодель, реализующая IWindowViewModel</param>
        /// <returns>Объект IWindow</returns>
        IWindow Create<TWindowViewModel>(TWindowViewModel viewModel)
            where TWindowViewModel : IWindowViewModel;
    }
}
