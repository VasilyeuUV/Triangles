using Triangles.Contracts.Settings.AboutWindowSettings;
using Triangles.Models.Infrastructure.Common;

namespace Triangles.Models.Settings.AboutWindowSettings
{
    /// <summary>
    /// Оболочка для настройек окна "О программе"
    /// </summary>
    internal class AboutWindowMementoWrapper : AWindowMementoWrapperBase<AboutWindowMemento>, IAboutWindowMementoWrapper
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="pathService"></param>
        public AboutWindowMementoWrapper(IPathService pathService)
            : base(pathService)
        {
        }

        protected override string MementoName => "AboutWindowMemento";
    }
}
