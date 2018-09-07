namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// Операция нажатия на кнопку
    /// </summary>
    [ToString]
    public class UIButton : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.Button;

        public bool MoveMouse { get; set; }

        /// <summary>
        /// Операция клика по кнопке
        /// </summary>
        public override void Automate(AutomationElement item)
        {
            var b = item.AsButton();
            b.Click(MoveMouse);
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }
    }
}