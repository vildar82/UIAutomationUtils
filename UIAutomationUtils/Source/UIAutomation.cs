namespace UIAutomationUtils
{
    using System;
    using Data;
    using JetBrains.Annotations;
    using NetLib;

    /// <summary>
    /// Вспомогательный класс для автоматизации UI
    /// </summary>
    [PublicAPI]
    public static class UIAutomation
    {
        /// <summary>
        /// Типы объектов для xml
        /// </summary>
        private static readonly Type[] xmlTypes =
        {
            typeof(UIButton),
            typeof(UICheckBox),
            typeof(UIRadioButton),
            typeof(UITree),
            typeof(UIPane),
            typeof(UiComboBox),
            typeof(UITextBox),
            typeof(UITab),
            typeof(UIWindow),
            typeof(UIContainer),
            typeof(UiListBox),
            typeof(UIMenu)
        };

        /// <summary>
        /// Загрузка задания из файла
        /// </summary>
        /// <param name="jobXmlFile">Файл задания xml</param>
        /// <returns>Задание автоматизации UI Windows</returns>
        public static UIJob LoadJob(string jobXmlFile)
        {
            return SerializerXml.Load<UIJob>(jobXmlFile, xmlTypes);
        }

        /// <summary>
        /// Сохранение задания автоматизации в файл xml
        /// </summary>
        /// <param name="job">Задане</param>
        /// <param name="file">Полный путь файла сохранения задания</param>
        public static void SaveJob([NotNull] this UIJob job, string file)
        {
            SerializerXml.Save(file, job, xmlTypes);
        }
    }
}