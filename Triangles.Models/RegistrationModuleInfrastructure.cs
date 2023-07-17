using Autofac;
using Triangles.Contracts.Settings.AboutWindowSettings;
using Triangles.Contracts.Settings.MainWindowSettings;
using Triangles.Models.Settings;
using Triangles.Models.Settings.AboutWindowSettings;
using Triangles.Models.Settings.MainWindowSettings;

namespace Triangles.Models
{
    /// <summary>
    /// Регистрационный модуль контейнера зависимостей в Информтруктуре
    /// </summary>
    public class RegistrationModuleInfrastructure : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // - Регистрация Wrapper главного окна под двумя интерфейсами
            builder.RegisterType<MainWindowMementoWrapper>()
                .As<IMainWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();
            // это значит, что контейнер сможет зарезолвить оба интерфейса и для обоих вернет один и тот же инстанс враппера,
            // но инстанс будет ограничен теми членами, которые описаны в интерфейсах  

            // - Регистрация Оболочки окна "О программе"
            builder.RegisterType<AboutWindowMementoWrapper>()
                .As<IAboutWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();

            //// - Регистрация провайдера версии приложения
            //builder.RegisterType<ApplicationVersionProvider>()
            //    .As<IApplicationVersionProvider>()
            //    .SingleInstance();

            //// - Регистрация RestApiExecutor
            //builder.RegisterType<ApiRequestExecutor>()
            //    .As<IApiRequestExecutor>()
            //    .SingleInstance();

        }
    }
}
