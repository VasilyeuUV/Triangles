using Newtonsoft.Json;
using Triangles.Contracts.Settings;
using Triangles.Models.Infrastructure.Common;

namespace Triangles.Models.Settings
{
    /// <summary>
    /// Базовый абстрактный класс Wrapper-а для Memento окон
    /// </summary>
    internal abstract class AWindowMementoWrapperBase<TMemento>
        : IWindowMementoWrapper, IWindowMementoWrapperInitializer, IDisposable
        where TMemento : AWindowMementoBase, new()
    {
        private readonly IPathService _pathService;
        private bool _isInitialized;
        private string _settingsFilePath = string.Empty;
        private TMemento _windowMemento;


        /// <summary>
        /// CTOR
        /// </summary>
        protected AWindowMementoWrapperBase(IPathService pathService)   // - protected, т.к. класс абстрактный
        {
            this._pathService = pathService;
            this._windowMemento = new TMemento();
        }


        /// <summary>
        /// Наименование Memento
        /// </summary>
        protected abstract string MementoName { get; }


        /// <summary>
        /// Проверка, были ли Wrapper инициализирован (защита, чтобы гарантировать инициализацию). 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void EnshureInitialized()
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"Wrapper for {typeof(TMemento)} is not initialized");
        }


        //############################################################################################################
        #region IWindowMementoWrapper

        public double Left
        {
            get
            {
                EnshureInitialized();
                return _windowMemento.Left;
            }
            set
            {
                EnshureInitialized();
                _windowMemento.Left = value;
            }
        }


        public double Top
        {
            get
            {
                EnshureInitialized();
                return _windowMemento.Top;
            }
            set
            {
                EnshureInitialized();
                _windowMemento.Top = value;
            }
        }


        public double Width
        {
            get
            {
                EnshureInitialized();
                return _windowMemento.Width;
            }
            set
            {
                EnshureInitialized();
                _windowMemento.Width = value;
            }
        }


        public double Height
        {
            get
            {
                EnshureInitialized();
                return _windowMemento.Height;
            }
            set
            {
                EnshureInitialized();
                _windowMemento.Height = value;
            }
        }


        public bool IsMaximized
        {
            get
            {
                EnshureInitialized();
                return _windowMemento.IsMaximized;
            }
            set
            {
                EnshureInitialized();
                _windowMemento.IsMaximized = value;
            }
        }

        #endregion // IWindowMementoWrapper


        //############################################################################################################
        #region IWindowMementoWrapperInitializer

        public void Initialize()
        {
            if (_isInitialized)
                throw new InvalidOperationException($"Wrapper for {typeof(TMemento)} is already initialized");

            _isInitialized = true;

            const string settingsFolderName = "Settings";

            var settingsPath = Path.Combine(_pathService.ApplicationFolder, settingsFolderName);
            _settingsFilePath = Path.Combine(settingsPath, $"{MementoName}.json");

            Directory.CreateDirectory(settingsPath);

            if (!File.Exists(_settingsFilePath))
                return;

            var serializedMemento = File.ReadAllText(_settingsFilePath);
            _windowMemento = JsonConvert.DeserializeObject<TMemento>(serializedMemento)
                ?? throw new InvalidOperationException($"Deserialized memento can't be null");      // - решение ошибки проверки на null

        }

        #endregion // IWindowMementoWrapperInitializer



        //############################################################################################################
        #region IDisposable

        public void Dispose()
        {
            EnshureInitialized();

            // - сохранение настроек окна при закрытии программы
            var serializedMemento = JsonConvert.SerializeObject(_windowMemento);
            File.WriteAllText(_settingsFilePath, serializedMemento);
        }

        #endregion // IDisposable
    }
}
