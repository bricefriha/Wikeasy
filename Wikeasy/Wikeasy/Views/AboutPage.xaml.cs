using System;
using System.ComponentModel;
using Wikeasy.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wikeasy.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AboutViewModel();
        }
    }
}