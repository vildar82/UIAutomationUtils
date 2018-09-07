namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// список
    /// </summary>
    public class UiListBox : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.List;

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc />
        public override void Automate(AutomationElement item)
        {
            item.AsListBox().Select(Value);
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }
    }
}