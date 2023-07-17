using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using Triangles.Models.Infrastructure.Common;

namespace Triangles.Bootstrapper.Logging
{
    /// <summary>
    /// Инициализатор логгера
    /// </summary>
    internal class LogManagerInitializer : ILogManagerInitializer, IDisposable
    {
        private readonly IPathService _pathService;

        /// <summary>
        /// CTOR
        /// </summary>
        public LogManagerInitializer(IPathService pathService)
        {
            this._pathService = pathService;

            var loggingConfiguration = CreateLoggingConfiguration();
            LogManager.Configuration = loggingConfiguration;
        }


        /// <summary>
        /// Настройка конфигурации логгера
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private LoggingConfiguration CreateLoggingConfiguration()
        {
            var logginConfiguration = new LoggingConfiguration();

            var appLoggingRule = CreateAppLogingRule();
            logginConfiguration.AddRule(appLoggingRule);

            return logginConfiguration;
        }


        /// <summary>
        /// Правила конфигурации 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private LoggingRule CreateAppLogingRule()
        {
            // - файловый таргет, отвечает за запись логов в файл
            var appLogFileTarget = new FileTarget()
            {
                FileName = Path.Combine(_pathService.ApplicationFolder, "Logs", "app.log")      // - путь к файлу
            };

            // - асинхронный target, позволяет накапливать и асинхронно записывать пакет логов
            var asyncTargetWrapper = new AsyncTargetWrapper(appLogFileTarget)
            {
                TimeToSleepBetweenBatches = 1000                                                // - задержка между записями в лог-файл
            };

            // - "*" - правило применяется ко всем логгерам в приложении
            // - Info - минимальный уровень логов для этого правила
            var loggingRule = new LoggingRule("*", LogLevel.Info, asyncTargetWrapper);
            return loggingRule;
        }


        //######################################################################################################################
        #region IDisposable

        public void Dispose()
        {
            LogManager.Flush();         // - сбросит все закэшированные логи в файл
            LogManager.Shutdown();
        }

        #endregion // IDisposable
    }
}
