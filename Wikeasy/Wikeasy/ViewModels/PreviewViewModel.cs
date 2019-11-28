using HtmlAgilityPack;
using System;

using Wikeasy.Models;
using Wikeasy.Services;

namespace Wikeasy.ViewModels
{
    public class PreviewViewModel : BaseViewModel
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
        public PreviewViewModel(string searchInput)
        {
            _service = new WikipediaApiDataService();

            _searchResult = new SearchResult();

            // Get the seach result
            GenerateSearchResult(searchInput);


        }
        /// <summary>
        /// Method allowing to generate a search result
        /// </summary>
        private async void GenerateSearchResult(string searchInput)
        {
            _isLoading = true;

            WikiData data = await _service.GetWikiData(searchInput);

            // set the page title
            this.Title = data.displaytitle;

            // Get the html
            string html = data.sections[0]["text"];
            // Instanciate the HTML doc
            HtmlDocument doc = new HtmlDocument();
            // Loading the doc
            doc.LoadHtml(html);

            // Get the main node
            HtmlNode documentNode = doc.DocumentNode;

            // Instanciate the model
            //_searchResult = new SearchResult()
            //{
            //    Img = ;
            //};

            _isLoading = false;
            
        }
    }
}
