using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheKhabar
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        
        public App(string databaseLocation)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDE4ODA3QDMxMzgyZTM0MmUzMGU5bWx3alJtNmYvN0tHTktBNnc1TExyRW9zQVl2S21RVk5HRlVmMjBlMTA9");

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
