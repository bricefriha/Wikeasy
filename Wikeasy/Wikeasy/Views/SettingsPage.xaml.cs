using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikeasy.Models;
using Wikeasy.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wikeasy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel _vm;
        public SettingsPage()
        {
            InitializeComponent();

            _vm = new SettingsViewModel();

            BindingContext = _vm;
        }

        private void themePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (themePicker.SelectedIndex != -1)
            {
                // Get selected theme name
                string themeName = themePicker.Items[themePicker.SelectedIndex];

                // Switch to the theme
                ((ThemeSwitch)_vm.Themes.Where(theme => theme.Name == themeName).FirstOrDefault()).Command.Execute(null);
            }
                
        }
    }
}