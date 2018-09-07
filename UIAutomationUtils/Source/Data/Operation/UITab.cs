namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// Переключение вкладок
    /// </summary>
    [ToString]
    public class UITab : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.Tab;

        /// <summary>
        /// Имя вкладки
        /// </summary>
        public string Tab { get; set; }

        /// <summary>
        /// Переключение на вкладку
        /// </summary>
        public override void Automate(AutomationElement item)
        {
            item.AsTab().SelectTabItem(Tab);
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }

        /// <inheritdoc />
        public override bool IsSatisfy(AutomationElement item)
        {
            return item.AsTab().SelectedTabItem?.Name == Tab;
        }
    }
}