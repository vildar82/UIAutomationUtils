namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// Выпадающий список - установка значения
    /// </summary>
    [ToString]
    public class UiComboBox : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.ComboBox;

        /// <summary>
        /// Значение из списка
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc />
        public override void Automate(AutomationElement item)
        {
            var cmb = item.AsComboBox();
            cmb.Select(Value);
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }

        /// <inheritdoc />
        public override bool IsSatisfy(AutomationElement item)
        {
            var cmb = item.AsComboBox();
            try
            {
                return cmb.Value == Value;
            }
            catch
            {
                return false;
            }
        }
    }
}