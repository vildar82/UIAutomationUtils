namespace UIAutomationUtils
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using CommandLine;
    using Data;
    using JetBrains.Annotations;

    public static class UIAutomationService
    {
        static UIAutomationService()
        {
            UiAutoExe = Assembly.GetExecutingAssembly().Location;
        }

        internal static string UiAutoExe { get; set; }

        public static string LogFile { get; set; }

        public static void AutomateJob(UIJob job)
        {
            LogConfig.Configure(job.LogFile);
            UIProgram.Automate(job);
        }

        [NotNull]
        public static Process StartUIProcess([NotNull] UIJob job, [NotNull] out string jobFile)
        {
            jobFile = NetLib.IO.Path.GetTempFile(".xml");
            job.SaveJob(jobFile);
            return StartUIProcess(jobFile);
        }

        [NotNull]
        public static Process StartUIProcess([NotNull] string jobFile)
        {
            var args = new Parser().FormatCommandLine(new UIOptions
            {
                JobFile = jobFile,
            });

            KillUIProcess();
            var uiPi = new ProcessStartInfo(UiAutoExe)
            {
                WorkingDirectory = Path.GetDirectoryName(UiAutoExe),

                // RedirectStandardOutput = true,
                // RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = args
            };
            var uiProcess = Process.Start(uiPi);
            if (uiProcess == null)
            {
                throw new Exception("Ошибка. Не удалось запустить процесс UIAutomationUtils.exe.");
            }

            Thread.Sleep(300);
            return uiProcess;
        }

        private static void KillUIProcess()
        {
            foreach (var process in Process.GetProcessesByName("UIAutomationUtils"))
            {
                process.Kill();
            }
        }
    }
}