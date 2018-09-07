namespace UIAutomationUtils.Data
{
    using System;
    using System.Collections.Generic;
    using FlaUI.Core.AutomationElements;
    using JetBrains.Annotations;
    using NLog;

    /// <inheritdoc />
    public abstract class UIContainerBase : IUIContainer
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public List<UiOperationBase> Operations { get; set; }

        private static ILogger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Обработка операций окна
        /// </summary>
        public abstract void Automate(AutomationElement win);

        /// <summary>
        /// Обработка операций
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="container"></param>
        protected static void AutomateOperation([CanBeNull] UiOperationBase operation, AutomationElement container)
        {
            if (operation == null)
                return;
            Logger.Info($"Start Operation - {operation.GetInfo()}");

            if (operation.Condition != null && operation.Condition.IsSatisfy(operation.Condition.GetItem(container)))
            {
                Logger.Info($"Operation IsSatisfy by Condition - {operation.Condition}");
                return;
            }

            var item = operation.GetItem(container);
            if (operation.NeedResult)
            {
                Logger.Info($"Operation Result: {operation.ResultKey}={operation.Result}");
                UIProgram.Result.Result.Add(operation.ResultKey, operation.Result);
            }
            else if (!operation.IsSatisfy(item))
            {
                try
                {
                    operation.Automate(item);
                    Logger.Info("Operation Automate");
                }
                catch (Exception ex)
                {
                    Logger.Info($"Operation Automate Fail - {ex.Message}");
                }
            }
            else
            {
                Logger.Info("Operation IsSatisfy");
            }

            operation.Container?.Automate(container);
        }
    }
}