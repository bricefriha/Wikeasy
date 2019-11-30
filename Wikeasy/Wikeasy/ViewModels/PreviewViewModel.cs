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


        }
    }
}
