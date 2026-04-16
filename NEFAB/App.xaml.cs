using System.Configuration;
using System.Data;
using System.Windows;
using NEFAB.Stores;
using NEFAB.ViewModels;

namespace NEFAB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationStore _navigationStore;

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = new HomeViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

    }

}
