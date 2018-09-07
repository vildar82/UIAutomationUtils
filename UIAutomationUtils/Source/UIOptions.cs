namespace UIAutomationUtils
{
    using CommandLine;

    /// <summary>
    /// Опции командной строки
    /// </summary>
    public class UIOptions
    {
        /// <summary>
        /// Файл задания для автаматизации окна - json
        /// </summary>
        [Option('f', "JobFile", Required = true, HelpText = "Файл задания для автаматизации окна (json).")]
        public string JobFile { get; set; }
    }
}