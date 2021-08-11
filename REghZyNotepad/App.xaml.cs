using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using REghZyNotepad.Core;
using REghZyNotepad.Dialogs;

namespace REghZyNotepad {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            WindowsDialogService service = new WindowsDialogService();
            ServiceLocator.SaveOpen = service;
            ServiceLocator.Dialog = service;
            ViewModelLocator.Instance.Application = new ApplicationViewModel();

            this.MainWindow = new MainWindow();
            this.MainWindow.Show();
        }
    }
}
