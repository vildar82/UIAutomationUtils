namespace UIAutomationUtils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Automation;
    using CommandLine;
    using Data;
    using FlaUI.Core;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Conditions;
    using FlaUI.Core.Identifiers;
    using FlaUI.Core.Input;
    using FlaUI.UIA2;
    using FlaUI.UIA3;
    using JetBrains.Annotations;
    using NetLib;
    using NLog;
    using ControlType = FlaUI.Core.Definitions.ControlType;
    using Debug = System.Diagnostics.Debug;
    using PropertyCondition = FlaUI.Core.Conditions.PropertyCondition;
    using TreeScope = FlaUI.Core.Definitions.TreeScope;

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
            Console.WriteLine("Нажми любую кнопку для старта:");
            Console.ReadKey();
            App = Application.Attach(uiJob.ProcessID);
            using (Auto = new UIA3Automation())
            {
                var mainWin = App.GetMainWindow(Auto);
                Console.WriteLine($"Главное окно - {mainWin.Name}");
                var elem = mainWin.FindFirstByXPath("/Pane[@Name='Область инструментов']/Pane[@Name='Навигатор']");
                Console.WriteLine($"{elem.Name}");
                var gl = elem.FindFirst(TreeScope.Children, elem.ConditionFactory.ByName("Навигатор:Главное представление"));
                Console.WriteLine($"{gl.Name}");
                var pane = gl.FindFirstByXPath("/Pane/Pane");
                var tree = pane.FindFirstChild(gl.ConditionFactory.ByControlType(ControlType.Tree)).AsTree();
                var tiShorts = tree.Items.FirstOrDefault(i => i.Name.StartsWith("Быстрые ссылки"));
                Console.WriteLine($"{tiShorts.Name}");
                tiShorts.Expand();
                var tiNets = tiShorts.Items.FirstOrDefault(i => i.Name.StartsWith("Трубопроводные сети"));
                Console.WriteLine($"{tiNets.Name}");
                tiNets.Expand();
                foreach (var item in tiNets.Items)
                {
                    Console.WriteLine($"Вставка быстрой ссылки {item.Name}");
                    item.Select();
                    item.RightClick();
                    Mouse.MoveBy(50, 5);
                    Mouse.Click(MouseButton.Left);
                    while (true)
                    {
                        var winLink = mainWin.ModalWindows.FirstOrDefault(w => w.Name == "Создать ссылку трубопроводной сети");
                        if (winLink == null)
                        {
                            Thread.Sleep(100);
                            continue;
                        }

                        var bOk = winLink.FindFirstByXPath("/Button[@Name='OK']").AsButton();
                        bOk.Click();
                        App.WaitWhileBusy();
                        Thread.Sleep(3000);
                        break;
                    }
                }

                Console.ReadKey();

                //uiJob.Container.Automate(mainWin);
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