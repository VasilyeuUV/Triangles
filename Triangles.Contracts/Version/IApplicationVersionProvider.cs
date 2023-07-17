namespace Triangles.Contracts.Version
{
    /// <summary>
    /// Контракт, позволяющий получить версию приложения
    /// </summary>
    public interface IApplicationVersionProvider
    {
        /// <summary>
        /// Версия приложения
        /// </summary>
        System.Version Version { get; }
    }
}
