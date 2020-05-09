using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Wikeasy.Helpers;
using Xamarin.Forms;

using Wikeasy.Models;
using Wikeasy.Views;
using HtmlAgilityPack;
using Wikeasy.Services;
using Newtonsoft.Json.Linq;
using System.Linq;
using static Wikeasy.Models.SearchResult;
using Wikeasy.Objects;
using Xamarin.Essentials;

namespace Wikeasy.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region constraint model
        
        // Subtitle
        private const string subtitleDefault = "What do you need to know?";
        private const string subtitleLoading = "Let me check...";
        private const string subtitleResult = "Here's what I found:";
        private const string subtitleDataNotFound = "Result not found, please try again:";

        // Not found
        private const string dataNotFound = "Not found.";

        // Search bar size
        private const double _height = 45;
        private const double _width = 365;

        #endregion
        static IWikipediaApiDataService _service;
        private SearchResult _searchResult;
        public SearchResult SearchResult
        {
            set
            {
                _searchResult = value;
                OnPropertyChanged();
            }
            get
            {
                return _searchResult;
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
            get
            {
                return _isLoading;
            }
        }

        private readonly Command _goToSource;
        public Command GoToSource
        {
            get
            {
                return _goToSource;
            }
        }

        private readonly Command _resultScroll;
        public Command ResultScroll
        {
            get
            {
                return _resultScroll;
            }
        }

        private readonly Command _searchTappedItem;
        public Command SearchTappedItem
        {
            get
            {
                return _searchTappedItem;
            }
        }

        private bool _isOtherResult;
        public bool IsOtherResult
        {
            set
            {
                _isOtherResult = value;
                OnPropertyChanged();
            }
            get
            {
                return _isOtherResult;
            }
        }

        private bool _isResultSomebody;
        public bool IsResultSomebody
        {
            set
            {
                _isResultSomebody = value;
                OnPropertyChanged();
            }
            get
            {
                return _isResultSomebody;
            }
        }
        private bool _isResultAvailable;
        public bool IsResultAvailable
        {
            set
            {
                _isResultAvailable = value;
                OnPropertyChanged();
            }
            get
            {
                return _isResultAvailable;
            }
        }

        private string _subtitle;
        public string Subtitle
        {
            set
            {
                _subtitle = value;
                OnPropertyChanged();
            }
            get
            {
                return _subtitle;
            }
        }

        private HomePage _page;
        public HomePage Page
        {
            //set
            //{
            //    _page = value;
            //    OnPropertyChanged();
            //}
            get
            {
                return _page;
            }
        }
        /// <summary>
        /// Constructor for UnitTest
        /// </summary>
        public HomeViewModel()
        {

        }

        /// <summary>
        /// Actual constructor
        /// </summary>
        /// <param name="page"></param>
        public HomeViewModel(HomePage page)
        {
            _isResultAvailable = false;
            // Get the page
            _page = page;

            _service = new WikipediaApiDataService();

            _searchResult = new SearchResult();

            _goToSource = new Command(url => Launcher.OpenAsync(new Uri(url.ToString())));

            // Command to search an item that we tapped
            _searchTappedItem = new Command(async title => await GenerateSearchResult(title.ToString())); 

            // Set scroll command
            //_resultScroll = new Command();

            // Default values
            //
            // Set the title
            Title = "Home";
            // Set the subtitle
            _subtitle = subtitleDefault;
            _isLoading = false;
            _isResultSomebody = false;
            
        }


        /// <summary>
        /// Method allowing to generate a search result
        /// </summary>
        public async Task GenerateSearchResult(string searchInput)
        {
            // Clear the result
            this.IsResultAvailable = false;
            //HomePage page = (HomePage)App.Current.MainPage;

            // Animation
            //
            // search bar disappearance
            await Page.FadeSearchBar(true);
            //
            //double width = Convert.ToDouble(Page.Resources["searchbarWidth"]);
            //double height = Convert.ToDouble(Page.Resources["searchbarHeight"]);

            // Annimation forward
            Page.AnimateWidthSearchBar(_width, _height);

            // Set the subtitle
            Subtitle = subtitleLoading;
            
            // Set the loading status as true
            IsLoading = true;

            // Verify if the input is not null
            if (!string.IsNullOrEmpty(searchInput))
            {
                // Define a new Data result
                DataResult dataResult = new DataResult(await _service.GetWikiData(WkeToolbox.FiltringInputSearch(searchInput)));


                if (dataResult.Wikidata is null)
                    Subtitle = subtitleDataNotFound;
                else
                {
                    // Get the search result after building it
                    SearchResult = dataResult.BuildResult();

                    // Set the Default subtitle
                    Subtitle = subtitleResult;

                    // Set the result as available
                    IsResultAvailable = true;

                    // 
                    IsResultSomebody = true;
                }
            }
            else
            {
                // Set the subtitle
                Subtitle = subtitleDefault;
            }

            // Set the loading status as false
            IsLoading = false;


            // Do we get a result?
            if (IsResultAvailable)
            {
                // Annimation backward
                Page.AnimateSearchBar(_height, _width, _height, /*height +*/ Page.Height);

                // Show result on the page
                Page.RevealingResult();

            }
            else
                Page.AnimateWidthSearchBar(_height, _width);

            // Search bar disappearance
            await Page.FadeSearchBar(false);


        }
        /// <summary>
        /// Reset the search
        /// </summary>
        public void ResetSearch()
        {
            // Set the result as unavailable
            IsResultAvailable = false;

            // Switch subtitle
            Subtitle = subtitleDefault;
        }
        /// <summary>
        /// Get all possible solution from an amguity
        /// </summary>
        /// <param name="description">Disambiguation page description</param>
        /// <returns></returns>
        public ObservableCollection<string> GetAmguitySolutions(string description)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            // Instanciate the HTML doc
            HtmlDocument doc = new HtmlDocument();

            // Loading the doc
            doc.LoadHtml(description);

            HtmlNode baseNode = doc.DocumentNode.SelectSingleNode("//ul");

            // Moving through solutions
            foreach (HtmlNode solution in baseNode.SelectNodes("//li //a"))
            {
                // Add the solution
                result.Add(solution.InnerText);
            }

            // return the all list
            return result;
        }


    }
}