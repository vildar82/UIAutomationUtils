namespace UIAutomationUtils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using CommandLine;
    using Data;
    using FlaUI.Core;
    using FlaUI.Core.AutomationElements;
    using FlaUI.UIA2;
    using FlaUI.UIA3;
    using JetBrains.Annotations;
    using NetLib;
    using NLog;

    /// <summary>
    /// Class Entry Point
    /// </summary>
    public static class UIProgram
    {
        /// <summary>
        /// Результаты
        /// </summary>
        public static readonly UIResult Result = new UIResult();

        private static ILogger Logger { get; } = LogManager.GetCurrentClassLogger();

        public static Application App { get; set; }

        public static AutomationBase Auto { get; private set; }

        /// <summary>
        /// Main Entry Point
        /// </summary>
        /// <param name="args">Опции запуска - файл задания</param>
        public static void Main([NotNull] string[] args)
        {
            var curDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException();
            Assembly.LoadFrom(Path.Combine(curDir, "Castle.Core.dll"));
#if DEBUG
            Console.WriteLine("Debug - для продолжения нажми любую кнопку");
            Console.ReadKey();
#endif
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Parser.Default.ParseArguments<UIOptions>(args)
                .WithParsed(RunOptionsAndReturnExitCode)
                .WithNotParsed(HandleParseError);
            Logger.Info("Exit");
        }

        private static void CurrentDomain_UnhandledException(object sender, [NotNull] UnhandledExceptionEventArgs e)
        {
            Logger.Error($"Exception - {e.ExceptionObject}");
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            var err = $"UIAutomationUtils args parse error: {string.Join(",", errs.Select(s => s.ToString()))}";
            Logger.Error(err);
            throw new ArgumentException(err);
        }

        private static void RunOptionsAndReturnExitCode([NotNull] UIOptions opts)
        {
            if (string.IsNullOrEmpty(opts.JobFile) || !File.Exists(opts.JobFile))
            {
                throw new FileNotFoundException("Не найден файл задания", opts.JobFile);
            }

            var uiJob = UIAutomation.LoadJob(opts.JobFile);
            LogConfig.Configure(uiJob.LogFile);
            Automate(uiJob);
            SaveResult(uiJob.ResultFile);
        }

        internal static void Automate(UIJob uiJob)
        {
            App = Application.Attach(uiJob.ProcessID);
            using (Auto = new UIA2Automation())
            {
                var mainWin = App.GetMainWindow(Auto);
                uiJob.Container.Automate(mainWin);
            }
        }

        private static bool IsAcadWin(Window win)
        {
            try
            {
                return win.Title.StartsWith("Autodesk AutoCAD");
            }
            catch
            {
                return false;
            }
        }

        private static void SaveResult([CanBeNull] string fileResult)
        {
            if (!Result.Result.Any())
                return;

            if (string.IsNullOrEmpty(fileResult))
            {
                Logger.Warn("Не задан файл результата в аргументах запуска.");
                return;
            }

            try
            {
                Result.Serialize(fileResult);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"SaveResult to '{fileResult}'.");
            }
        }
    }
}