using System.Windows.Input;
using Triangles.Contracts.Factories;
using Triangles.ViewModels.Commands;
using Triangles.ViewModels.Windows.AboutWindow;

namespace Triangles.ViewModels.Windows.MainWindow.MainWindowMenuViewModel
{
    /// <summary>
    /// ViewModel меню главного окна
    /// </summary>
    public class MainWindowMenuViewModel : IMainWindowMenuViewModel
    {
        private readonly IWindowManager _windowManager;                                             // - менеджер окон

        // - вьюмодели
        private IAboutWindowViewModel? _aboutWindowViewModel;                                       // - вьюмодель окна "О программе"

        // - фабрики
        private readonly IFactory<IAboutWindowViewModel> _aboutWindowViewModelFactory;              // - фабрика для вьюмодели окна "О программе"
        //private readonly IFactory<IAuthorCollectionViewModel> _authorCollectionViewModelFactory;    // - фабрика вьюмодели коллекции авторов

        // - команды
        private readonly Command _closeMainWindowCommand;                    // - команда закрытия главного окна
        private readonly Command _openAboutWindowCommand;                    // - команда открытия окна "О программе"
        private readonly AsyncCommand _loadDataAsyncCommand;                 // - команда открытия файла и загрузки данных
        private readonly Command _throwExceptionCommand;                     // - команда имитации исключительной ситуации


        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowMenuViewModel(
            IWindowManager windowManager,
            IFactory<IAboutWindowViewModel> aboutWindowViewModelFactory
            //IFactory<IAuthorCollectionViewModel> authorCollectionViewModelFactory
            )
        {
            this._windowManager = windowManager;
            this._aboutWindowViewModelFactory = aboutWindowViewModelFactory;
            //this._authorCollectionViewModelFactory = authorCollectionViewModelFactory;

            _closeMainWindowCommand = new Command(CloseMainWindow);
            _openAboutWindowCommand = new Command(OpenAboutWindow);
            _loadDataAsyncCommand = new AsyncCommand(LoadDataAsync);
            _throwExceptionCommand = new Command(() => throw new Exception("Test exception"));
        }


        //######################################################################################################################
        #region EVENTS

        /// <summary>
        /// Действия на событие закрытия окна "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnAboutWindowClosed(object? sender, EventArgs e)
        {
            // - отписываемся от события и обнуляем текущщий экземпдяр вьюмодел
            if (sender is IWindow window)
            {
                window.Closed -= OnAboutWindowClosed;
                _aboutWindowViewModel = null;
            }
        }

        #endregion // EVENTS


        /// <summary>
        /// Закрытие главного окна
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CloseMainWindow()
        {
            MainWindowClosingRequested?.Invoke();
        }


        /// <summary>
        /// Открыть окно "О программе"
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OpenAboutWindow()
        {
            if (_aboutWindowViewModel == null)
            {
                _aboutWindowViewModel = _aboutWindowViewModelFactory.Create();
                var aboutWindow = _windowManager.Show(_aboutWindowViewModel);
                aboutWindow.Closed += OnAboutWindowClosed;
                return;
            }
            else
            {
                _windowManager.Show(_aboutWindowViewModel);
            }

        }


        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task LoadDataAsync()
        {
            //var authorCollectionViewModel = _authorCollectionViewModelFactory.Create();
            //await authorCollectionViewModel.InitializeAsync();                          // - здесь отработает логика запроса данных от REST-сервиса

            //ContentViewModelChanged?.Invoke(authorCollectionViewModel);
        }



        ///// <summary>
        ///// Получение списка авторов книг
        ///// </summary>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //private async Task OpenAuthorCollectionAsync()
        //{
        //    var authorCollectionViewModel = _authorCollectionViewModelFactory.Create();
        //    await authorCollectionViewModel.InitializeAsync();                          // - здесь отработает логика запроса данных от REST-сервиса

        //    ContentViewModelChanged?.Invoke(authorCollectionViewModel);
        //}


        //######################################################################################################################
        #region IMainWindowMenuViewModel

        public event Action? MainWindowClosingRequested;
        public event Action<IMainWindowContentViewModel>? ContentViewModelChanged;

        public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
        public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;
        public ICommand ThrowExceptionCommand => _throwExceptionCommand;
        public ICommand LoadDataAsyncCommand => _loadDataAsyncCommand;

        public void CloseAboutWindow()
        {
            if (_aboutWindowViewModel != null)
                _windowManager.Close(_aboutWindowViewModel);        // - закрытие окна "О программе"
        }


        #endregion // IMainWindowMenuViewModel
    }
}
