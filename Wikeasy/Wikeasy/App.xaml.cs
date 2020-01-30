using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wikeasy.Services;
using Wikeasy.Views;
using Wikeasy.ViewModels;
using Wikeasy.Themes;

namespace Wikeasy
{
    public partial class App : Application
    {
        private static AppViewModel _vm;
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();

            _vm = new AppViewModel();
            BindingContext = _vm;
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
        public void SetTheme(Theme theme)
        {
            //Handle Light Theme & Dark Theme

            switch (theme)
            {
                // If the system theme is dark
                case Theme.Dark:
                    // Switch to dark theme
                    //_vm.ThemeUrl = "/Themes/DarkTheme.xaml";
                    App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                    break;
                // If the system theme is light
                case Theme.Light:
                    // Switch to light theme
                    //_vm.ThemeUrl = "/Themes/LightTheme.xaml";
                    App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                    break;
            }
        }
    }
}
