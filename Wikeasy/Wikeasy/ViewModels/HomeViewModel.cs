using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Wikeasy.Models;
using Wikeasy.Views;

namespace Wikeasy.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
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
                return this._searchResult; 
            }
        }
        
        public HomeViewModel()
        {
            Title = "Home";
        }

        /// <summary>
        /// Method allowing to generate a search result
        /// </summary>
        private void GenerateSearchResult()
        {
            
        }
    }
}