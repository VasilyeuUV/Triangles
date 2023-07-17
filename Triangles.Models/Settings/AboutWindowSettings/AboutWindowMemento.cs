namespace Triangles.Models.Settings.AboutWindowSettings
{
    /// <summary>
    /// Настройки окна "О программе"
    /// </summary>
    internal class AboutWindowMemento : AWindowMementoBase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public AboutWindowMemento()
        {
            this.Left = 100;
            this.Top = 100;
            this.Width = 600;
            this.Height = 400;
            this.IsMaximized = false;
        }
    }
}
