namespace UIAutomationUtils.Data
{
    using System.Collections.Generic;
    using FlaUI.Core.AutomationElements;

    /// <summary>
    /// Контейнер контролов - окно, панель
    /// </summary>
    public interface IUIContainer
    {
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Операции
        /// </summary>
        List<UiOperationBase> Operations { get; set; }

        /// <summary>
        /// Выполнение операций
        /// </summary>
        /// <param name="root">Родительский элемент (окно и т.п.)</param>
        void Automate(AutomationElement root);
    }
}