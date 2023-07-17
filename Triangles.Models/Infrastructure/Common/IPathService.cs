namespace Triangles.Models.Infrastructure.Common
{
    /// <summary>
    /// Контракт сервиса для работы с путями к файлам и каталогам
    /// </summary>
    public interface IPathService
    {
        /// <summary>
        /// Путь к каталогу приложения для сохранения настроек (для потребителя)
        /// </summary>
        string ApplicationFolder { get; }
    }
}
