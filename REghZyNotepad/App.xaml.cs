using System;
using System.Runtime.InteropServices;
using System.Windows;
using REghZyNotepad.Core;
using REghZyNotepad.Dialogs;

namespace REghZyNotepad {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;

            WindowsDialogService service = new WindowsDialogService();
            ServiceLocator.SaveOpen = service;
            ServiceLocator.Dialog = service;
            ViewModelLocator.Instance.Application = new ApplicationViewModel();

            this.MainWindow = new MainWindow();
            this.MainWindow.Show();

            if (e.Args != null && e.Args.Length > 0) {
                ProcessArgs(e.Args);
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
                OpenArgsFile(parms.Replace('\"', '\0'));
            }
        }

        private void OpenArgsFile(string file) {
            ViewModelLocator.Instance.GetCurrentNotepad().OpenDocument(file);
        }
    }
}
