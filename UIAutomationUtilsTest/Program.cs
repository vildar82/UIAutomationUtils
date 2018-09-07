namespace UIAutomationUtilsTest
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using UIAutomationUtils;
    using UIAutomationUtils.Data;

    class Program
    {
        static void Main(string[] args)
        {
            UIAutomationService.AutomateJob(GetNavigatorJob());

            //UIAutomationService.LogFile = @"c:\temp\UiAutomation.log";
            //UIAutomationService.StartUIProcess(GetTestJob(), out _, out _);
        }

        private static UIJob GetAcdUsersEditJob()
        {
            return new UIJob
            {
                ProcessID = Process.GetProcessesByName("AcadUtilsEditUsers")[0].Id,
                Container = new UIWindow
                {
                    Operations = new List<UiOperationBase>
                    {
                        new UiComboBox { Value = "АР" }
                    }
                }
            };
        }

        private static UIJob GetTestJob()
        {
            return new UIJob
            {
                ProcessID = Process.GetProcessesByName("notepad")[0].Id,
                Container = new UIWindow
                {
                    Operations = new List<UiOperationBase>
                    {
                        new UIMenu { Childs = new[] { "Файл", "Выход" } }
                    }
                }
            };
        }

        private static UIJob GetNavigatorJob()
        {
            return new UIJob
            {
                ProcessID = Process.GetProcessesByName("acad")[0].Id,
                Container = new UIContainer()
                {
                    Name = "Область инструментов",
                    Operations = new List<UiOperationBase>
                    {
                        new UIPane { ControlName = "Навигатор" }
                    }
                }
            };
        }

        private static UIJob GetIntersectionsUiJob()
        {
            var coridorRadio = "Создать новый коридор ";

            // Сохранение задания автоматизации во временный файл
            return new UIJob
            {
                ProcessID = Process.GetProcessesByName("acad")[0].Id,
                Container = new UIWindow
                {
                    Name = "Создать перекресток - Общие",
                    Operations = new List<UiOperationBase>
                    {
                        // Общие
                        new UiComboBox { ControlName = "Тип коридора перекрестка: ", Value = "Сохранение гребней на всех" },

                        // Детали геометрии
                        new UIButton { ControlName = "Детали геометрии" },
                        new UICheckBox { ControlName = "Создать или задать трассы для смещения ", Checked = true },
                        new UICheckBox { ControlName = "Создать трассы для сопряжения ", Checked = true },

                        // new UIButton{ ControlName ="Параметры смещения ", Window = new UIWindow
                        // {
                        //    // Окно смещений
                        //    WindowName = "Перекресток - Параметры смещения",
                        //    Operations = new List<UiOperationBase>
                        //    {
                        //        // Величина смещения - у всех 3м !!!???
                        //        new UICheckBox{ControlName = "Создание новых смещений от начала до конца осевых линий", Checked = true},
                        //        new UIButton{ ControlName = "OK" }
                        //    }
                        // }},
                        new UICheckBox
                        {
                            ControlName = "Создать профили для смещения и профили для сопряжения ",
                            Checked = true
                        },

                        // Области коридора
                        new UIButton { ControlName = "Области коридора" },
                        new UICheckBox { ControlName = "Создать коридоры в зоне перекрестка ", Checked = true },
                        new UIRadioButton { ControlName = coridorRadio },
                        new UIButton
                        {
                            ControlName = "Обзор...",
                            Container = new UIWindow
                            {
                                Name = "Выбор файла набора конструкций",
                                Operations = new List<UiOperationBase>
                                {
                                    new UITextBox
                                    {
                                        ControlName = "Имя файла:",
                                        Value =
                                            @"\\picompany.ru\pikp\lib\03_Civil 3D\06_Библиотеки\02_Наборы конструкций\AutoIntersections.xml"
                                    },
                                    new UIButton { ControlName = "Открыть" }
                                }
                            }
                        },
                        new UIButton { ControlName = "Создать перекресток" }
                    }
                }
            };
        }
    }
}