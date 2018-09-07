namespace UIAutomationUtils.Data
{
    /// <summary>
    /// Задание автоматизации окна - передается через json файл.
    /// </summary>
    public class UIJob
    {
        /// <summary>
        /// Идентификатор процесса
        /// </summary>
        public int ProcessID { get; set; }

        /// <summary>
        /// Обрабатываемое окно
        /// </summary>
        public UIContainerBase Container { get; set; }

        /// <summary>
        /// Файл лога
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// Полный путь файла результатов
        /// </summary>
        public string ResultFile { get; set; }
    }
}