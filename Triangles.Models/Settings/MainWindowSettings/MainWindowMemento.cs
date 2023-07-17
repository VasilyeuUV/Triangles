namespace Triangles.Models.Settings.MainWindowSettings
{
    /// <summary>
    /// Класс, в котором храним настройки главного окна
    /// </summary>
    internal class MainWindowMemento : AWindowMementoBase       // - Memento - окончание для классов настроек
    {

        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowMemento()
        {
            this.Left = 100;
            this.Top = 100;
            this.Width = 600;
            this.Height = 400;
            this.IsMaximized = true;
        }
    }
}
