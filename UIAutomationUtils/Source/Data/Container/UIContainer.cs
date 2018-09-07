namespace UIAutomationUtils.Data
{
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    /// <inheritdoc />
    public class UIContainer : UIContainerBase
    {
        /// <inheritdoc />
        public override void Automate(AutomationElement root)
        {
            var container = root.FindFirst(TreeScope.Children, root.ConditionFactory.ByName(Name));
            foreach (var operation in Operations)
            {
                AutomateOperation(operation, container);
            }
        }
    }
}
