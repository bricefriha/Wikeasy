using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Wikeasy.Models;
using Wikeasy.Views;
using HtmlAgilityPack;
using Wikeasy.Services;
using Newtonsoft.Json.Linq;

namespace Wikeasy.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
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
        
        public HomeViewModel()
        {
            _service = new WikipediaApiDataService();

            _searchResult = new SearchResult();

            Title = "Home";
            _isLoading = false;
        }


        /// <summary>
        /// Method allowing to generate a search result
        /// </summary>
        public async void GenerateSearchResult(string searchInput)
        {
            IsLoading = true;
            var data = await _service.GetWikiData(searchInput);

            // set the page title
            Title = data.Lead.Displaytitle;

            // Get the html
            //string html = data.sections[0]["text"];
            // Instanciate the HTML doc
            //HtmlDocument doc = new HtmlDocument();
            //// Loading the doc
            //doc.LoadHtml(html);

            // Get the main node
            //HtmlNode documentNode = doc.DocumentNode;

            // Instanciate the model
            //_searchResult = new SearchResult()
            //{
            //    Img = ;
            //};

            IsLoading = false;

        }
    }
}