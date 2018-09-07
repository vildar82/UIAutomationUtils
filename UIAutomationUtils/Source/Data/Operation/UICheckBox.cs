namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// Операция нажатия на кнопку
    /// </summary>
    [ToString]
    public class UICheckBox : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.CheckBox;

        /// <summary>
        /// Значение
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Галочка
        /// </summary>
        public override void Automate(AutomationElement item)
        {
            item.AsCheckBox().IsChecked = Checked;
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            var item = GetItemBase(container);
            if (item == null)
            {
            }

            return item;
        }

        /// <inheritdoc />
        public override bool IsSatisfy(AutomationElement item)
        {
            return item.AsCheckBox().IsChecked == Checked;
        }
    }
}