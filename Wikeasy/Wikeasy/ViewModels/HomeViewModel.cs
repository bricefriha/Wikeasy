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
        
        public HomeViewModel()
        {
            _service = new WikipediaApiDataService();

            _searchResult = new SearchResult();

            // Default values
            //
            // Set the title
            Title = "Home";
            // Set the subtitle
            _subtitle = subtitleDefault;
            _isLoading = false;
            _isResultSomebody = false;
            _isResultAvailable = false;
        }


        /// <summary>
        /// Method allowing to generate a search result
        /// </summary>
        public async Task GenerateSearchResult(string searchInput)
        {
            // Set the subtitle
            Subtitle = subtitleLoading;
            
            // Set the loading status as true
            IsLoading = true;

            //var data = await _service.GetWikiData(WkeToolbox.FiltringInputSearch(searchInput));
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
            

            // Set the loading status as false
            IsLoading = false;


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
        
    }
}