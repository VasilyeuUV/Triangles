namespace Triangles.Contracts.Settings
{
    /// <summary>
    /// Базоый интерфейс для настроек окон
    /// </summary>
    public interface IWindowMementoWrapper
    {
        // 1. Свойства (как и у Memento)

        /// <summary>
        /// Координата окна по горизонтали
        /// </summary>
        double Left { get; set; }

        /// <summary>
        /// Координата окна по вертикали
        /// </summary>
        double Top { get; set; }

        /// <summary>
        /// Ширина окна
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Высота окна
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Флаг состояния окна развёрнутого в полный экран (true)
        /// </summary>
        bool IsMaximized { get; set; }
    }
}
