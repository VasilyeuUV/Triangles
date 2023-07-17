using System.ComponentModel;

namespace Triangles.ViewModels.Windows
{
    /// <summary>
    /// Контракт для работы с окнами
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Событие перед закрытием окна
        /// </summary>
        event CancelEventHandler Closing;

        /// <summary>
        /// Событие при закрытии окна
        /// </summary>
        event EventHandler Closed;


        /// <summary>
        /// Показ окна
        /// </summary>
        void Show();


        /// <summary>
        /// Закрытие окна
        /// </summary>
        void Close();


        /// <summary>
        /// Активация существующего окна
        /// </summary>
        /// <returns></returns>
        bool Activate();
    }
}
