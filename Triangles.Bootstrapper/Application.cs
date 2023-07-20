using Autofac;
using NLog;
using System.Windows.Forms;
using Triangles.Contracts.Factories;
using Triangles.Models;
using Triangles.Models.Settings;
using Triangles.ViewModels;
using Triangles.ViewModels.Windows;
using Triangles.ViewModels.Windows.MainWindow;
using Triangles.Views;

namespace Triangles.Bootstrapper
{
    internal class Application : IApplication, IDisposable
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(Application));       // - логгер

        private readonly ILifetimeScope _applicationLifetimeScope;      // - в качестве контейнера зависимостей, определяет время жизни объектов в контейнере
        private IMainWindowViewModel? _mainWindowViewModel;             // - вьюмодель главного окна приложения


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="lifetimeScope"></param>
        public Application(ILifetimeScope lifetimeScope)
        {
            Logger.Info("Created");

            // - этот класс будет зависеть от root-ового LifetimeScope (все остальные зависимости приложения будут создаваться в дочернем)
            this._applicationLifetimeScope = lifetimeScope.BeginLifetimeScope(RegisterDependencies);
        }




        /// <summary>
        /// Регистрация модулей в контейнере зависимостей
        /// </summary>
        /// <param name="containerBuilder"></param>
        private static void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            // Регистрация существующих модулей
            containerBuilder
                .RegisterModule<RegistrationModuleInfrastructure>()
                .RegisterModule<RegistrationModuleBootstrapper>()
                .RegisterModule<RegistrationModuleViews>()
                .RegisterModule<RegistrationModuleViewModels>();
        }



        /// <summary>
        /// Инициализация зависимостей
        /// </summary>
        private void InitializeDependencies()
        {
            // - получение коллекции Wrapper-ов
            var windowMementoWrapperInitializers = _applicationLifetimeScope.Resolve<IEnumerable<IWindowMementoWrapperInitializer>>();
            foreach (var windowMementoWrapperInitializer in windowMementoWrapperInitializers)
                windowMementoWrapperInitializer.Initialize();                           // - инициализация Wrapper-а Memento окна приложения   
        }


        //#################################################################################################################
        #region IApplication

        public Form Run()
        {
            InitializeDependencies();                                                                   // - инициализация зависимостей

            var windowManager = _applicationLifetimeScope.Resolve<IWindowManager>();                    // - резолв менеджера окна

            // - резолвим фабрику для вьюмодели главного окна
            var mainWindowViewModelFactory = _applicationLifetimeScope.Resolve<IFactory<IMainWindowViewModel>>();
            this._mainWindowViewModel = mainWindowViewModelFactory.Create();                            // - создаём вьюмодель главного окна с помощью фабрики

            var mainWindow = windowManager.Show(this._mainWindowViewModel);                             // - создание и показ окна

            // Проверим, кстится ли это к классу Form. Если нет, то exception
            if (mainWindow is not Form form)
                throw new NotImplementedException();

            return form;
        }

        #endregion // IApplication


        //#################################################################################################################
        #region IDisposable

        public void Dispose()
        {
            _mainWindowViewModel?.Dispose();            // - освобождаем вьюмодель главного окна
            _applicationLifetimeScope?.Dispose();       // - освобождаем контейнер

            Logger.Info("Disposed");
        }

        #endregion // IDisposable
    }
}
