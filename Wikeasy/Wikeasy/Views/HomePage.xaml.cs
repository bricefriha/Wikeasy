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
        double width;
        double height;
        HomeViewModel _vm;
        #region constraint
        const float SbDefaultCornerRadius = 100;
        const float SbActiveCornerRadius = 15;

        #endregion

        public HomePage()
        {
            InitializeComponent() ;

            BindingContext = this._vm = new HomeViewModel();
        }

        async void txtSearch_Completed(object sender, EventArgs e)
        {
            // Animation
            //
            // search bar disappearance
            await FadeSearchBar(sender, true);
            //
            width = frameSearchBar.Width;
            height = frameSearchBar.Height;

            // Annimation forward
            AnimateWidthSearchBar(width, height);

            // Seaching process
            await _vm.GenerateSearchResult(((Entry)sender).Text);

            // Do we get a result?
            if (_vm.IsResultAvailable)
            {
                // Annimation backward
                AnimateSearchBar(height, width, height, height + 500);

                // Change de corner radius
                frameSearchBar.CornerRadius = SbActiveCornerRadius;
            }
            else
                AnimateWidthSearchBar(height, width);

            // Search bar disappearance
            await FadeSearchBar(sender, false);
        }
        /// <summary>
        /// Entry focus event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_Focused(object sender, FocusEventArgs e)
        {
            // If a result has been shown
            if (_vm.IsResultAvailable)
            {
                // Reset the SearchBar Height 
                AnimateHeightSearchBar(frameSearchBar.Height, height, 60, 150);

                

                _vm.ResetSearch();

                // Change de corner radius
                frameSearchBar.CornerRadius = SbDefaultCornerRadius;
            }

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
        private void AnimateWidthSearchBar(double startingWidth, double endingWidth)
        {
            // update the height of the layout with this callback
            Action<double> callback = input => { frameSearchBar.WidthRequest = input; };

            // pace at which aniation proceeds
            uint rate = 16;

            // one second animation
            const uint length = 700; 
            Easing easing = Easing.Linear;

            frameSearchBar.Animate("invis", callback, startingWidth, endingWidth, rate, length, easing);
        }
        /// <summary>
        /// Method allowing the searchbar annimation
        /// </summary>
        /// <param name="startingHeight">Start size</param>
        /// <param name="endingHeight">End size</param>
        /// <param name="rate">pace at which aniation proceeds</param>
        /// <param name="length">The number of milliseconds over which to interpolate the animation</param>
        private void AnimateHeightSearchBar(double startingHeight, double endingHeight, uint rate, uint length)
        {
            // update the height of the layout with this callback
            Action<double> callback = input => { frameSearchBar.HeightRequest = input; };
            
            
            Easing easing = Easing.Linear;

            frameSearchBar.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing);

            
        }

        /// <summary>
        /// Method allowing the searchbar annimation
        /// </summary>
        /// <param name="startingHeight">Start size</param>
        /// <param name="endingHeight">End size</param>
        private void AnimateSearchBar( double startingWidth, double endingWidth, double startingHeight, double endingHeight)
        {
            // pace at which aniation proceeds
            uint rate = 16; 
            // one second animation
            uint length = 700; 
            Easing easing = Easing.Linear;
            // update the height of the layout with this callback
            Action<double> callbackWidth = input => { frameSearchBar.WidthRequest = input; }; 
            
            // Width anniation
            frameSearchBar.Animate("invisWidth", callbackWidth, startingWidth, endingWidth, rate, length, easing);
            // update the height of the layout with this callback
            Action<double> callbackHeight = input => { frameSearchBar.HeightRequest = input; }; 

            // Height anniation
            frameSearchBar.Animate("invisHeight", callbackHeight, startingHeight, endingHeight, rate, length, easing);

            

        }

    }
}