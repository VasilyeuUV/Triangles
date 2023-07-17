using Triangles.Contracts.Settings.AboutWindowSettings;
using Triangles.Contracts.Version;

namespace Triangles.ViewModels.Windows.AboutWindow
{
    /// <summary>
    /// Вьюмодель окна "О программе"
    /// </summary>
    public class AboutWindowViewModel : AWindowViewModelBase<IAboutWindowMementoWrapper>, IAboutWindowViewModel
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="windowMementoWrapper"></param>
        public AboutWindowViewModel(
            IAboutWindowMementoWrapper windowMementoWrapper,
            IApplicationVersionProvider applicationVersionProvider
            )
            : base(windowMementoWrapper)
        {

            Version = $"Version {applicationVersionProvider.Version.ToString(3)}";
        }


        /// <summary>
        /// Версия приложения
        /// </summary>
        public string Version { get; }
    }
}
