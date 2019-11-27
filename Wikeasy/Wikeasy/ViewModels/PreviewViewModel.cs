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
        public PreviewViewModel()
        {
            _service = new WikipediaApiDataService();

            _searchResult = new SearchResult();

            
        }
        /// <summary>
        /// Method allowing to generate a search result
        /// </summary>
        private async void GenerateSearchResult(string searchInput )
        {
            WikiData data = await _service.GetWikiData(searchInput);

            // Get the html
            string html = data.sections[0]["text"];
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode documentNode = doc.DocumentNode;

            // Instanciate the model
            //_searchResult = new SearchResult()
            //{
            //    Img = ;
            //};
            
        }
    }
}
