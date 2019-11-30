using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Wikeasy.Models;
using Wikeasy.Views;
using Wikeasy.ViewModels;

namespace Wikeasy.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = this.viewModel = new HomeViewModel();
        }

        async void txtSearch_Completed(object sender, EventArgs e)
        {
            await ((Entry)sender).FadeTo(0, 5000, Easing.Linear);
            await icoSearch.FadeTo(0, 5000, Easing.Linear);
            double startingHeight = frameSearchBar.Width;
            double endingHeight = frameSearchBar.Height;
            Action<double> callback = input => { frameSearchBar.WidthRequest = input; }; // update the height of the layout with this callback
            uint rate = 16; // pace at which aniation proceeds
            uint length = 1000; // one second animation
            Easing easing = Easing.Linear;
            //var animate = new Animation(d => frameSearchBar.WidthRequest = d, 100);
            frameSearchBar.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing);

            //animate.Commit(frameSearchBar, "BarGraph", 40, 25, Easing.SpringIn);
            // Navigate the the preview page
            //Navigation.PushAsync(new PreviewPage(((Entry)sender).Text));
        }
    }
}