using Autofac;
using Triangles.ViewModels.Windows;
using Triangles.Views.Windows;
using Triangles.Views.Windows.AboutWindow;
using Triangles.Views.Windows.MainWindow;

namespace Triangles.Views
{
    /// <summary>
    /// Класс регистрации в контейнере вью-сборки
    /// </summary>
    public class RegistrationModuleViews : Module
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder">Контейнер для регистрации</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<WindowManager>()           // - регистрация менеджера окна
                .As<IWindowManager>()
                .SingleInstance();

            builder.RegisterType<MainWindow>()              // - регистрируемый тип (главное окно)
                .As<IMainWindow>()                          // - контракт вьюшки (интерфейс, под которым регистрируем тип)
                .InstancePerDependency();                   // - время жизни вьюшки (такой же как и для вьюмодели)
            builder.RegisterType<AboutWindow>()             // - регистрация окна "О программе" 
                .As<IAboutWindow>()
                .InstancePerDependency();
        }
    }
}
