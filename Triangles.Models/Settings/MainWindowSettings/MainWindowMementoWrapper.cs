using Triangles.Contracts.Settings.MainWindowSettings;
using Triangles.Models.Infrastructure.Common;

namespace Triangles.Models.Settings.MainWindowSettings
{
    /// <summary>
    /// Класс, объект которого будет отвечать за загрузку, сохранение и чтение настроек главного окна в файл JSON
    /// (Класс оболочки для Memento)
    /// </summary>
    internal class MainWindowMementoWrapper : AWindowMementoWrapperBase<MainWindowMemento>, IMainWindowMementoWrapper
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowMementoWrapper(IPathService pathService)
            : base(pathService) { }


        //############################################################################################################
        #region AWindowMementoWrapperBase

        protected override string MementoName => "MainWindowMemento";

        #endregion // AWindowMementoWrapperBase
    }
}
