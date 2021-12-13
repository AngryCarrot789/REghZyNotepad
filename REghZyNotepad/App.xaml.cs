using System;
using System.Runtime.InteropServices;
using System.Windows;
using DragonJetzNotepad.Core;
using DragonJetzNotepad.Dialogs;

namespace DragonJetzNotepad {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;

            ViewModelLocator.Init();
            WindowsDialogService service = new WindowsDialogService();
            ServiceLocator.SaveOpen = service;
            ServiceLocator.Dialog = service;

            this.MainWindow = new MainWindow();
            this.MainWindow.Show();

            if (e.Args != null && e.Args.Length > 0) {
                this.ProcessArgs(e.Args);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            try {
                ServiceLocator.Dialog.Show("Crashed!", $"The application has crashed: {e.ExceptionObject.ToString()}");
            }
            catch { }
        }

        private void ProcessArgs(string[] args) {
            string parms = string.Join(" ", args);
            if (parms.Length > 0) {
                this.OpenArgsFile(parms.Replace('\"', '\0'));
            }
        }

        private void OpenArgsFile(string file) {
            ViewModelLocator.Instance.GetCurrentNotepad().OpenDocument(file);
        }
    }
}
