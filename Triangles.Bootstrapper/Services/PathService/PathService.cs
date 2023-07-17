using Triangles.Models.Infrastructure.Common;

namespace Triangles.Bootstrapper.Services.PathService
{
    /// <summary>
    /// Сервис для работы с путями к файлам и каталогам
    /// </summary>
    internal class PathService : IPathService, IPathServiceInitializer
    {
        private bool _isInitialized;                        // - флаг, что Wrapper был инициализирован (false /отсутствие инициализации/ по умолчанию)
        private string _applicationFolder = string.Empty;   // - путь к каталогу приложения для сохранения настроек


        //############################################################################################################
        #region IPathService


        public string ApplicationFolder
        {
            get
            {
                EnshureInitialized();
                return _applicationFolder;
            }
            private set => _applicationFolder = value;
        }

        #endregion // IPathService


        //############################################################################################################
        #region IPathServiceInitializer

        public void Initialize()
        {
            // - проверим, не был ли Wrapper уже инициализирован
            if (_isInitialized)
                throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");

            _isInitialized = true;

            // - ищем settings файл, чтобы загрузить из него данные
            // - Microsoft рекомендует хранить настройки приложения в профиле Пользователя, который находится в папке с именем Пользователя в папке Users на диске С. 
            var localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);   // - получаем имя папки Локальных данных профиля Пользователя

            // в этой папке принято создавать папку с наименованием компании, в которой создать папку с именем приложения, в которой иметь папку с настройками
            const string company = "DevTricks Channel";
            const string applicationName = "DevTricks App";

            // - формируем путь к папке настроек Приложения (именно через Path, т.к. учитывает слэши)
            ApplicationFolder = Path.Combine(localApplicationDataPath, company, applicationName);
        }

        #endregion // IPathServiceInitializer


        /// <summary>
        /// Проверка, были ли Wrapper инициализирован (защита, чтобы гарантировать инициализацию). 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void EnshureInitialized()
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"{nameof(IPathService)} is not initialized");
        }
    }
}
