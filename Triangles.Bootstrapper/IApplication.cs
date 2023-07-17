using System.Windows.Forms;

namespace Triangles.Bootstrapper
{
    /// <summary>
    /// Контракт для регистрации в контейнере
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Создание главного окна
        /// </summary>
        Form Run();
    }
}
