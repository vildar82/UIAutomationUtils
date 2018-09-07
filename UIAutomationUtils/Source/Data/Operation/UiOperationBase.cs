using NetLib;

namespace UIAutomationUtils.Data
{
    using System.Xml.Serialization;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [ToString]
    public abstract class UiOperationBase : IUIOperation
    {
        /// <summary>
        /// Тип контрола
        /// </summary>
        [XmlIgnore]
        public abstract ControlType ControlType { get; set; }

        /// <inheritdoc />
        public string ControlName { get; set; }

        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public UIContainerBase Container { get; set; }

        /// <inheritdoc />
        public UiOperationBase Condition { get; set; }

        /// <inheritdoc />
        public object Result { get; set; }

        /// <inheritdoc />
        public string ResultKey { get; set; }

        /// <inheritdoc />
        public bool NeedResult { get; set; }

        /// <inheritdoc />
        public abstract void Automate(AutomationElement item);

        /// <inheritdoc />
        public abstract AutomationElement GetItem(AutomationElement container);

        /// <inheritdoc />
        public virtual bool IsSatisfy(AutomationElement item)
        {
            return false;
        }

        /// <summary>
        /// Инфа
        /// </summary>
        /// <returns></returns>
        [NotNull]
        public string GetInfo()
        {
            return $"{ControlType}, {ControlName}";
        }

        /// <summary>
        /// поиск контрола
        /// </summary>
        /// <param name="container">Окно (панель)</param>
        /// <returns>Контрол</returns>
        protected AutomationElement GetItemBase([NotNull] AutomationElement container)
        {
            AutomationElement res = null;
            try
            {
                res = ControlName.IsNullOrEmpty()
                    ? container.FindAllByXPath($@"/{ControlType}")[Index]
                    : container.FindFirstByXPath($@"/{ControlType}[@Name='{ControlName}']");
            }
            catch
            {
            }

            return res ?? container.FindFirst(TreeScope.Children, container.ConditionFactory.ByName(ControlName));
        }
    }
}