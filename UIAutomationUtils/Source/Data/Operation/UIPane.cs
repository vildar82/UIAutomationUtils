namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// Переключение вкладок
    /// </summary>
    [ToString]
    public class UIPane : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.Pane;

        /// <summary>
        /// Переключение на вкладку
        /// </summary>
        public override void Automate(AutomationElement item)
        {
            var pane = item.FindFirst(TreeScope.Children, item.ConditionFactory.ByName(ControlName));
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }
    }
}