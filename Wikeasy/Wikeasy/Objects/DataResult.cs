﻿using HtmlAgilityPack;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Collections;
using TMDbLib.Objects.General;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using Wikeasy.Helpers;
using Wikeasy.Models;

namespace Wikeasy.Objects
{
    public class DataResult
    {
        private const string _wikipediaBaseUrl = "https://en.wikipedia.org/wiki/";
        private const string _blueSquare = "<table class=\"box-More_citations_needed plainlinks metadata ambox ambox-content ambox-Refimprove\" role=\"presentation\"><tbody><tr><td class=\"mbox-image\">";

        // Attributs
        private ResultType _resultType;
        private WikiData _wikidata;
        private SearchResult _actualResult;

        public DataResult(WikiData wikidata)
        {
            _wikidata = new WikiData();
            _wikidata = wikidata;
        }

        public ResultType ResultType { get => _resultType;}
        public WikiData Wikidata { get => _wikidata; /*set => _wikidata = value; */}
        public SearchResult ActualResult { get => _actualResult; /*set => _actualResult = value;*/ }

        /// <summary>
        /// Set the result type according to the wikidata
        /// </summary>
        /// <param name="birthdate">birthdate</param>
        private void SetResultType(string birthdate)
        {

            bool descriptionExist = !string.IsNullOrEmpty(_wikidata.Lead.Description);
            // ToDo: I feel there is a way to optimise all these stuffs
            if (birthdate != null)
            {
                // it's a movie star
                if (descriptionExist && (_wikidata.Lead.Description.Contains("filmmaker") || _wikidata.Lead.Description.Contains("actor")|| _wikidata.Lead.Description.Contains("actress")))
                    _resultType = ResultType.MovieStar;

                // other wise
                else
                    _resultType = ResultType.Person;
            }
            else
            {
                // is a movie
                if (descriptionExist && (_wikidata.Lead.Description.Contains("film") || _wikidata.Lead.Description.Contains("movie")))
                    _resultType = ResultType.Movie;
                else if (descriptionExist && _wikidata.Lead.Description == "Disambiguation page providing links to topics that could be referred to by the same search term")
                    _resultType = ResultType.Ambiguity;

                // Otherwise
                else
                    _resultType = ResultType.Other;
            }
        }
        /// <summary>
        /// Set and return the search result
        /// </summary>
        /// <returns>the actual result</returns>
        public SearchResult BuildResult()
        {
            // Get the html
            string html = _wikidata.Lead.Sections[0].Text;

            // Instanciate the HTML doc
            HtmlDocument doc = new HtmlDocument();

            // Loading the doc
            doc.LoadHtml(html);

            // Get the main node
            HtmlNode documentNode = doc.DocumentNode;

            // Get the birthdate
            string birthdate = documentNode.SelectNodes("//*[@class='bday']")?[0].InnerText;

            // Set result type
            this.SetResultType(birthdate);
            DateTime birthdateDt = new DateTime();
            bool birthdayFilled = false;


            if (!string.IsNullOrEmpty(birthdate))
            {
                birthdayFilled = BirthdayFilled(birthdate);

                if (birthdayFilled)
                {
                    // get birthday in date time
                    birthdateDt = DateTime.Parse(birthdate);

                }
                else
                {
                    birthdate = birthdate.Replace("-00", string.Empty);

                    // get birthday in date time
                    birthdateDt = DateTime.Parse(birthdate);
                }
            }
            
            

            string displaytitle = WkeToolbox.HtmlEscape(_wikidata.Lead.Displaytitle);
            // Set actual result
            switch (_resultType)
            {
                // Set movie star property
                case ResultType.MovieStar:

                    // Get Tmdb information
                    string movieStarName = _wikidata.Lead.Displaytitle.Split('(',')')[0];

                    // Get the client
                    TMDbClient client = new TMDbClient(App.Current.Resources["TmdbKey"].ToString());

                    // Get a collection from the research
                    SearchContainer<SearchPerson> collectons = client.SearchPersonAsync(movieStarName).Result;

                    // Get person information
                    var movieStar = collectons.Results.First();
                    MovieCredits movieStarCredit = client.GetPersonMovieCreditsAsync(movieStar.Id).Result;
                    string name = movieStar.Name;

                    

                    _actualResult = new MovieStarResult()
                    {
                        Img = _wikidata.Lead.Image.Urls["640"].ToString(),
                        Title = name,
                        Description = _wikidata.Lead.Description,
                        CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")?[0].InnerText,
                        Age = GetAgeFromBirthdate(birthdateDt),
                        Birthdate = birthdayFilled ? DateTime.Parse(birthdate).ToString("MMMM dd, yyyy") : DateTime.Parse(birthdate).ToString("MMMM, yyyy"),
                        Birthplace = documentNode.SelectNodes("//*[@class='birthplace']")?[0].InnerText,
                        Deathplace = documentNode.SelectNodes("//*[@class='deathplace']")?[0].InnerText,
                        Residence = documentNode.SelectNodes("//*[@class='label']")?[0].InnerText,
                        Type = _resultType,
                        KnownFor = movieStar.KnownFor,
                        WikipediaLink = _wikipediaBaseUrl + displaytitle.Replace(' ', '_'),
                        TmdbLink = ("https://www.themoviedb.org/" + movieStar.MediaType.ToString() + "/" + movieStarCredit.Id + "-" + name.Replace(' ', '-')).ToLower(),
                        SeenOn = movieStarCredit.Cast.OrderBy(movie => movie.ReleaseDate).Reverse().ToList(),

                    };
                    break;
                // Set movie star property
                case ResultType.Movie:

                    // Get Tmdb information
                    string movieTitle = displaytitle;

                    // Get the client
                    TMDbClient clientMovie = new TMDbClient(App.Current.Resources["TmdbKey"].ToString());

                    // Get a collection from the research
                    SearchContainer<SearchMovie> movieInfo = clientMovie.SearchMovieAsync(movieTitle).Result;

                    // Get person information
                    var movie = movieInfo.Results.First();

                    var movieCredit = clientMovie.GetMovieCreditsAsync(movie.Id).Result;
                    //var movieGenre = clientMovie.GetGenreMoviesAsync(movie.Id).Result;
                    string movieName = movie.Title;
                    _actualResult = new MovieResult()
                    {
                        Img = "https://image.tmdb.org/t/p/w600_and_h900_bestv2" + movie.PosterPath,
                        Title = movieName,
                        Description = movie.Overview,
                        Casts = movieCredit.Cast,
                        Rate = movie.VoteAverage.ToString() + "/10",
                        ReleaseDate = movie.ReleaseDate.Value,
                        WikipediaLink = _wikipediaBaseUrl + displaytitle.Replace(' ', '_'),
                        TmdbLink = ("https://www.themoviedb.org/"+ movie.MediaType.ToString() + "/" + movieCredit.Id + "-" + movieName.Replace(' ', '-')).ToLower(),
                        Type = _resultType,
                    };
                    break;

                // Set Person property
                case ResultType.Person:
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image?.Urls["640"].ToString(),
                        Title = displaytitle,
                        Description = _wikidata.Lead.Description,
                        CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")?[0].InnerText,
                        Age = GetAgeFromBirthdate(birthdateDt),
                        Birthdate = birthdayFilled? DateTime.Parse(birthdate).ToString("MMMM dd, yyyy"): DateTime.Parse(birthdate).ToString("MMMM, yyyy"),
                        Birthplace = documentNode.SelectNodes("//*[@class='birthplace']")?[0].InnerText,
                        Deathplace = documentNode.SelectNodes("//*[@class='deathplace']")?[0].InnerText,
                        Residence = documentNode.SelectNodes("//*[@class='label']")?[0].InnerText,
                        WikipediaLink = _wikipediaBaseUrl + displaytitle.Replace(' ', '_'),
                        Type = _resultType,

                    };
                    break;
                case ResultType.Other:
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image?.Urls["640"].ToString(),
                        Title = displaytitle,
                        Description = _wikidata.Lead.Description,
                        Type = _resultType,
                        WikipediaLink = _wikipediaBaseUrl + displaytitle.Replace(' ', '_'),
                        ExtendedDescription = htmlDoc.DocumentNode.InnerText,

                    };
                    
                    break;
                case ResultType.Ambiguity:
                    _actualResult = new SearchResult()
                    {
                        Title = displaytitle,
                        AmbiguitySolutions = this.GetAmguitySolutions(html),
                        Type = _resultType,

                    };
                    
                    break;
                // Otherwise... to avoid crashes...
                default:
                    _actualResult = new SearchResult();
                    break;
            }
            return _actualResult;
        }
        /// <summary>
        /// Verifying is the birthday is filled in the birthdate
        /// </summary>
        /// <param name="birthdate">birtdate</param>
        /// <returns>yes or no</returns>
        private bool BirthdayFilled ( string birthdate)
        {
            // Split the date
            var fields = birthdate.Split('-');

            return fields[2] == "00" ? false : true; 
        }
        /// <summary>
        /// Getting the age of a person from his birthdate
        /// </summary>
        /// <param name="birthdate">Birthdate</param>
        /// <returns>Age</returns>
        private string GetAgeFromBirthdate (DateTime birthdate)
        {
            var today = DateTime.Today;
            var todayDay = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birthDay = (birthdate.Year * 100 + birthdate.Month) * 100 + birthdate.Day;

            return ((todayDay - birthDay) / 10000).ToString();

        }
        /// <summary>
        /// Get all possible solution from an amguity
        /// </summary>
        /// <param name="description">Disambiguation page description</param>
        /// <returns></returns>
        private ObservableCollection<string> GetAmguitySolutions(string description)
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
