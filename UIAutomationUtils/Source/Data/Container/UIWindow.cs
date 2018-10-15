namespace UIAutomationUtils.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;
    using JetBrains.Annotations;
    using NLog;

    /// <summary>
    /// Описание окна и операций автоматизируемым в нем
    /// </summary>
    public class UIWindow : UIContainerBase
    {
        private static ILogger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <inheritdoc />
        public override void Automate(AutomationElement parentWin)
        {
            if (parentWin is Window window)
            {
                var win = GetWindow(window);
                foreach (var operation in Operations)
                {
                    AutomateOperation(operation, win);
                }
            }
        }

        [NotNull]
        private Window GetWindow(Window window)
        {
            Window win = null;
            while (win == null)
            {
                try
                {
                    win = window.ModalWindows.FirstOrDefault(w => w.Title == Name);
                    Logger.Info($"win = {win?.Name}");

                    if (win == null)
                    {
                        win = window.FindFirstChild(window.ConditionFactory.ByControlType(ControlType.Window)
                            .And(window.ConditionFactory.ByName(Name)))?.AsWindow();
                    }

                    if (win == null)
                    {
                        win = UIProgram.App.GetAllTopLevelWindows(UIProgram.Auto).FirstOrDefault(w => w.Title == Name);
                    }

                    if (win != null)
                        return win;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"GetWindow Exception {Name}. Parent window '{window.Name}'.");
                }

                Thread.Sleep(150);
            }

            return win;
        }
    }
}