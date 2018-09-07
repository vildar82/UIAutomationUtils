namespace UIAutomationUtils.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Данные результатов работы автоматизации
    /// </summary>
    public class UIResult
    {
        /// <summary>
        /// Результаты
        /// </summary>
        public Dictionary<string, object> Result { get; set; } = new Dictionary<string, object>();
    }
}