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
        HomeViewModel _vm;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = this._vm = new HomeViewModel();
        }

        async void txtSearch_Completed(object sender, EventArgs e)
        {
            // Animation
            //
            // search bar disappearance
            await FadeSearchBar(sender, true);
            //
            double width = frameSearchBar.Width;
            double height = frameSearchBar.Height;

            // Annimation forward
            AnimateSearchBar(width, height);

            // Seaching process
            await _vm.GenerateSearchResult(((Entry)sender).Text);

            // Annimation backward
            AnimateSearchBar(height, width);

            // search bar disappearance
            await FadeSearchBar(sender, false);
        }
        /// <summary>
        /// Method allowing the searchBar disappearance/appearance
        /// </summary>
        /// <param name="sender">Search bar</param>
        /// <param name="ToZero">true = disappearance // false = appearance </param>
        /// <returns></returns>
        private async Task FadeSearchBar(object sender, bool ToZero)
        {
            const int Length = 100;
            if (ToZero)
            {
                // Disappearance
                await ((Entry)sender).FadeTo(0, Length, Easing.Linear);
                await icoSearch.FadeTo(0, Length, Easing.Linear);
            }
            else
            {
                // Appearance
                await ((Entry)sender).FadeTo(1, Length, Easing.Linear);
                await icoSearch.FadeTo(1, Length, Easing.Linear);
            }
            
        }
        /// <summary>
        /// Method allowing the searchbar annimation
        /// </summary>
        /// <param name="startingHeight">Start size</param>
        /// <param name="endingHeight">End size</param>
        private void AnimateSearchBar(double startingHeight, double endingHeight)
        {
            Action<double> callback = input => { frameSearchBar.WidthRequest = input; }; // update the height of the layout with this callback
            uint rate = 16; // pace at which aniation proceeds
            uint length = 700; // one second animation
            Easing easing = Easing.Linear;

            frameSearchBar.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing);
        }
    }
}