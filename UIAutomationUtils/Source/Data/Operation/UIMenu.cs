namespace UIAutomationUtils.Data
{
    using System.Linq;
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <summary>
    /// Переключение вкладок
    /// </summary>
    [ToString]
    public class UIMenu : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.MenuBar;

        /// <summary>
        /// Имя вкладки
        /// </summary>
        public string[] Childs { get; set; }

        /// <summary>
        /// Переключение на вкладку
        /// </summary>
        public override void Automate(AutomationElement item)
        {
            var menu = item.AsMenu();
            var menuItem = menu.Items[Childs[0]];
            foreach (var child in Childs.Skip(1))
            {
                menuItem = menuItem.Expand();
                menuItem = menuItem.Items[child];
            }

            menuItem.Click();
        }

        /// <summary>
        /// Menu
        /// </summary>
        /// <param name="container">Window</param>
        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            var win = (Window)container;
            win.Focus();
            return win.FindFirstChild(c => c.Menu()).AsMenu();
        }
    }
}