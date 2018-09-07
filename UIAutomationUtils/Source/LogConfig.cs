namespace UIAutomationUtils
{
    using NetLib;
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using NLog.Targets.Wrappers;

    /// <summary>
    /// Конфигкратор логов
    /// </summary>
    public static class LogConfig
    {
        /// <summary>
        /// Конфигкратор логов
        /// </summary>
        /// <param name="logFile"></param>
        public static void Configure(string logFile)
        {
            var config = LogManager.Configuration ?? new LoggingConfiguration();
#if DEBUG
            var debugTarget = new DebugTarget();
            config.AddTarget("debug", debugTarget);
            config.AddRuleForAllLevels(debugTarget);

            var consoleTarget = new ConsoleTarget();
            config.AddTarget("console", consoleTarget);
            config.AddRuleForAllLevels(consoleTarget);
            LogManager.Configuration = config;
#endif
            if (logFile.IsNullOrEmpty())
                return;
            try
            {
                // Настройка конфигурации логгера.
                var fileTarget = new FileTarget { FileName = logFile };
                var asyncFileTarget = new AsyncTargetWrapper(fileTarget);
                config.AddTarget("log", asyncFileTarget);
                config.AddRuleForAllLevels(asyncFileTarget);
                LogManager.Configuration = config;
            }
            catch
            {
            }
        }
    }
}