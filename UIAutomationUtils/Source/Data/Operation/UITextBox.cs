namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// EditControlType
    /// </summary>
    [ToString]
    public class UITextBox : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.Text;

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Ввод значения в поле
        /// </summary>
        public override void Automate(AutomationElement item)
        {
            var tb = item.AsTextBox();
            tb.FocusNative();
            tb.Enter(Value);
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }

        /// <inheritdoc />
        public override bool IsSatisfy(AutomationElement item)
        {
            try
            {
                var tb = item.AsTextBox();
                return tb.Text == Value;
            }
            catch
            {
                return false;
            }
        }
    }
}