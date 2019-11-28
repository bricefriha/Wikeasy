using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Wikeasy.Models;
using Wikeasy.ViewModels;

namespace Wikeasy.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PreviewPage : ContentPage
    {
        PreviewViewModel viewModel;

        public PreviewPage(PreviewViewModel viewModel)
        {

            BindingContext = this.viewModel = viewModel;
        }

        public PreviewPage(string searchInput)
        {
            InitializeComponent();

            BindingContext = this.viewModel = new PreviewViewModel(searchInput);
        }
    }
}