namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;
    using NetLib;

    /// <summary>
    /// Дерево
    /// </summary>
    [ToString]
    public class UITree : UiOperationBase
    {
        /// <inheritdoc />
        [XmlIgnore]
        public override ControlType ControlType { get; set; } = ControlType.Tree;

        /// <summary>
        /// Путь к узлу
        /// </summary>
        public string[] Path { get; set; }

        /// <summary>
        /// Значение узла
        /// </summary>
        public object Value { get; set; }

        /// <inheritdoc />
        public override void Automate(AutomationElement item)
        {
            var node = item.AsTree().FindFirstByXPath(Path.JoinToString(@"/")).AsTreeItem();
            node.Select();
        }

        /// <inheritdoc />
        public override AutomationElement GetItem(AutomationElement container)
        {
            return GetItemBase(container);
        }
    }
}