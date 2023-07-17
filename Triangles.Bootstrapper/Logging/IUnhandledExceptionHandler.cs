namespace Triangles.Bootstrapper.Logging
{
    /// <summary>
    /// Контракт обработки необработанных исключений
    /// </summary>
    public interface IUnhandledExceptionHandler
    {
        void Handle(UnhandledExceptionEventArgs args);
    }
}
