using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wikeasy.Services;
using Wikeasy.Views;

namespace Wikeasy
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            // Detect the theme
            base.OnStart();

            Theme theme = DependencyService.Get<IEnvironment>().GetOperatingSystemTheme();

            SetTheme(theme);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

            // Detect the theme
            base.OnStart();

            Theme theme = DependencyService.Get<IEnvironment>().GetOperatingSystemTheme();

            SetTheme(theme);
        }
        /// <summary>
        /// To handle Light Theme & Dark Theme
        /// </summary>
        /// <param name="theme"></param>
        void SetTheme(Theme theme)
        {
            //Handle Light Theme & Dark Theme
        }
    }
}
