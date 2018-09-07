namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// RadioButton
    /// </summary>
    [ToString]
    public class UIRadioButton : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.RadioButton;

        /// <inheritdoc />
        public override void Automate(AutomationElement item)
        {
            item.AsRadioButton().IsChecked = true;
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }

        /// <inheritdoc />
        public override bool IsSatisfy(AutomationElement item)
        {
            return item.AsRadioButton().IsChecked;
        }
    }
}