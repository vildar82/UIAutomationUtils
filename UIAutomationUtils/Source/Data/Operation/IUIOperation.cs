namespace UIAutomationUtils.Data
{
    using FlaUI.Core.AutomationElements;
    using JetBrains.Annotations;

    /// <summary>
    /// Автоматизируемая операция в окне - совершаемое для одного контрола
    /// </summary>
    public interface IUIOperation
    {
        /// <summary>
        /// Имя контрола (text)
        /// </summary>
        [CanBeNull]
        string ControlName { get; set; }

        /// <summary>
        /// Поиск по индексу элемента
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Если после этой операции откроется новое окно
        /// </summary>
        [CanBeNull]
        UIContainerBase Container { get; set; }

        /// <summary>
        /// Операция условия - если она IsSatisfy, то основную операцию можно пропустить.
        /// </summary>
        UiOperationBase Condition { get; set; }

        /// <summary>
        /// Результат
        /// </summary>
        object Result { get; set; }

        /// <summary>
        /// Ключевое имя результата
        /// </summary>
        string ResultKey { get; set; }

        /// <summary>
        /// Если нужно значение контрола
        /// </summary>
        bool NeedResult { get; set; }

        /// <summary>
        /// Выполнение операции
        /// </summary>
        void Automate([NotNull] AutomationElement item);

        /// <summary>
        /// Значения контрола соответствуют операции - не нужно менять значения контрола
        /// </summary>
        /// <returns></returns>
        bool IsSatisfy([NotNull] AutomationElement item);

        /// <summary>
        /// Поиск контрола
        /// </summary>
        /// <returns></returns>
        AutomationElement GetItem([NotNull] AutomationElement container);
    }
}