using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Wikeasy.Models;
using Wikeasy.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Wikeasy.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        // Themes
        private ObservableCollection<string> _themesString;
        public ObservableCollection<string> ThemesString
        {
            get
            {
                return _themesString;
            }
        }
        // selected Theme
        private string _selectedTheme;
        public string SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }
        }
        // Themes
        private ObservableCollection<ThemeSwitch> _themes;
        public ObservableCollection<ThemeSwitch> Themes
        {
            //set
            //{
            //    _themes = value;
            //    OnPropertyChanged();
            //}
            get
            {
                return _themes;
            }
        }
        public SettingsViewModel()
        {
            Title = "Settings";
            _themesString = new ObservableCollection<string>();
            _themes = new ObservableCollection<ThemeSwitch>()
            {
                
                // Dark mode
                new ThemeSwitch()
                {
                    Id = 2,
                    Name = "Default (Your smartphone's theme)",
                    Command = new Command(
                    execute: () =>
                    {
                        // Set the new theme
                        App.SetTheme(DependencyService.Get<IEnvironment>().GetOperatingSystemTheme());

                        // Store the theme
                        Preferences.Set("Theme", 2);
                    })
                },
                // Light mode
                new ThemeSwitch()
                {
                    Id = 0,
                    Name = "Light",
                    Command = new Command(
                    execute: () =>
                    {
                        const Theme light = Theme.Light;

                        // Set the new theme
                        App.SetTheme(light);

                        // Store the theme
                        Preferences.Set("Theme", 0);
                    })
                },
                // Dark mode
                new ThemeSwitch()
                {
                    Id = 1,
                    Name = "Dark",
                    Command = new Command(
                    execute: () =>
                    {
                        const Theme dark = Theme.Dark;

                        // Set the new theme
                        App.SetTheme(dark);

                        // Store the theme
                        Preferences.Set("Theme", 1);
                    })
                },

                
            };
            // Add all ThemesString 
            foreach (var theme in Themes)
            {
                _themesString.Add(theme.Name);
            }

            // Set selected theme
            _selectedTheme = ((ThemeSwitch)Themes.Where(theme => Preferences.Get("theme", 2) == theme.Id).FirstOrDefault()).Name;


        }

    }
}
