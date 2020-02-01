using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wikeasy.Services;
using Wikeasy.Views;
using Wikeasy.ViewModels;
using Wikeasy.Themes;
using Xamarin.Essentials;

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
        /// <summary>
        /// Handle when your app starts
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
            // Set the theme from the key stored in preferences
            SetFromThemeId(Preferences.Get("theme",2));

            // Detect the theme
            //base.OnStart();
            //Theme theme = DependencyService.Get<IEnvironment>().GetOperatingSystemTheme();

            //SetTheme(theme);
        }

        /// <summary>
        /// Handle when your app sleeps
        /// </summary>
        protected override void OnSleep()
        {
            // 
        }

        /// <summary>
        /// Handle when your app resumes
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            // Set the theme from the key stored in preferences
            SetFromThemeId(Preferences.Get("theme", 2));
        }

        /// <summary>
        /// To handle Light Theme & Dark Theme
        /// </summary>
        /// <param name="theme">Theme</param>
        public static void SetTheme(Theme theme)
        {
            //Handle Light Theme & Dark Theme
            switch (theme)
            {
                // If the system theme is dark
                case Theme.Dark:
                    // Switch to dark theme
                    App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                    break;
                // If the system theme is light
                case Theme.Light:
                    // Switch to light theme
                    App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                    break;
            }
        }
        /// <summary>
        /// Set the theme stored from the theme Id
        /// </summary>
        private void SetFromThemeId(int themeId)
        {

            // Choose a theme
            switch (themeId)
            {
                // Light
                case 0:
                    // Switch to light theme
                    App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                    break;
                // Dark
                case 1:
                    // Switch to light theme
                    App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                    break;
                // System
                case 2:
                    // Set the theme according to the system theme
                    SetTheme(DependencyService.Get<IEnvironment>().GetOperatingSystemTheme());
                    break;
            }
        }
    }
}
