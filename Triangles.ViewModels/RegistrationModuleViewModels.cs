using Autofac;
using Triangles.ViewModels.Extensions;
using Triangles.ViewModels.Windows.AboutWindow;
using Triangles.ViewModels.Windows.MainWindow;
using Triangles.ViewModels.Windows.MainWindow.MainWindowMenuViewModel;

namespace Triangles.ViewModels
{
    public class RegistrationModuleViewModels : Module
    {

        /// <summary>
        /// Регистрация модулей при загрузке
        /// </summary>
        /// <param name="builder">Контейнер для регистрации</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // - регистрация окон
            builder.RegisterViewModel<Windows.MainWindow.MainWindowViewModel, IMainWindowViewModel>();
            builder.RegisterViewModel<AboutWindowViewModel, IAboutWindowViewModel>();

            // - регистрация вьюмоделей главного окна
            //builder.RegisterViewModel<AuthorCollectionViewModel, IAuthorCollectionViewModel>();             // - контент главного окна
            builder.RegisterViewModel<MainWindowMenuViewModel, IMainWindowMenuViewModel>();                 // - меню главного окна
            //builder.RegisterViewModel<MainWindowStatusBarViewModel, IMainWindowStatusBarViewModel>();       // - строка состояния главного окна

        }
    }
}
