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
using System.Linq;

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

            var data = await _service.GetWikiData(searchInput);

            

            if (data is null)
                Subtitle = subtitleDataNotFound;
            else
            {
                // Set the page title
                Title = data.Lead.Displaytitle;

                // Get the html
                string html = data.Lead.Sections[0].Text;
                // Instanciate the HTML doc
                HtmlDocument doc = new HtmlDocument();
                // Loading the doc
                doc.LoadHtml(html);

                // Get the main node
                HtmlNode documentNode = doc.DocumentNode;

                // Get the birthdate
                string birthdate = documentNode.SelectNodes("//*[@class='bday']") is null ? null : documentNode.SelectNodes("//*[@class='bday']")[0].InnerText;

                // Instanciate the model
                SearchResult = new SearchResult()
                {
                    Img = data.Lead.Image.Urls["640"].ToString(),
                    Title = data.Lead.Displaytitle,
                    Description = data.Lead.Description,
                    CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']") is null ? null : documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")[0].InnerText,
                    Age = (DateTime.Now.Year - DateTime.Parse(birthdate).Year).ToString(),
                    Birthdate = DateTime.Parse(birthdate).ToString("MMMM dd, yyyy"),
                    Birthplace = documentNode.SelectNodes("//*[@class='birthplace']") is null ? null : documentNode.SelectNodes("//*[@class='birthplace']")[0].InnerText,
                    Deathplace = documentNode.SelectNodes("//*[@class='deathplace']") is null ? null : documentNode.SelectNodes("//*[@class='deathplace']")[0].InnerText,
                    Residence = documentNode.SelectNodes("//*[@class='label']") is null ? null : documentNode.SelectNodes("//*[@class='label']")[0].InnerText,

                };
                // Set the Default subtitle
                Subtitle = subtitleResult;

                // Set the result as available
                IsResultAvailable = true;
                IsResultSomebody = true;
            }
            

            // Set the loading status as false
            IsLoading = false;


        }
    }
}