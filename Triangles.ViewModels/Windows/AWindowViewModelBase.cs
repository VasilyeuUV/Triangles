using Triangles.Contracts.Settings;

namespace Triangles.ViewModels.Windows
{
    /// <summary>
    /// Базовый класс для оконных ViewModels
    /// </summary>
    public abstract class AWindowViewModelBase<TWindowMementoWrapper> : AViewModelBase, IWindowViewModel
        where TWindowMementoWrapper : class, IWindowMementoWrapper
    {
        private readonly TWindowMementoWrapper _windowMementoWrapper;


        /// <summary>
        /// CTOR
        /// </summary>
        protected AWindowViewModelBase(TWindowMementoWrapper windowMementoWrapper)
        {
            this._windowMementoWrapper = windowMementoWrapper;
        }


        ////############################################################################################################
        //#region Свойства окна

        ///// <summary>
        ///// Координата окна по горизонтали
        ///// </summary>
        //public double Left
        //{
        //    get => _windowMementoWrapper.Left;
        //    set => _windowMementoWrapper.Left = value;
        //}


        ///// <summary>
        ///// Координата окна по вертикали
        ///// </summary>
        //public double Top
        //{
        //    get => _windowMementoWrapper.Top;
        //    set => _windowMementoWrapper.Top = value;
        //}


        ///// <summary>
        ///// Ширина окна
        ///// </summary>
        //public double Width
        //{
        //    get => _windowMementoWrapper.Width;
        //    set => _windowMementoWrapper.Width = value;
        //}


        ///// <summary>
        ///// Высота окна
        ///// </summary>
        //public double Height
        //{
        //    get => _windowMementoWrapper.Height;
        //    set => _windowMementoWrapper.Height = value;
        //}


        ///// <summary>
        ///// Флаг состояния окна развёрнутого в полный экран (true)
        ///// </summary>
        //public bool IsMaximized
        //{
        //    get => _windowMementoWrapper.IsMaximized;
        //    set => _windowMementoWrapper.IsMaximized = value;
        //}

        //#endregion // Свойства окна


        //############################################################################################################
        #region IWindowViewModel


        public virtual void WindowClosing()
        {
        }

        #endregion // IWindowViewModel


    }
}
