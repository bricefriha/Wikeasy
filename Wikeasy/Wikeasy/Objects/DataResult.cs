using HtmlAgilityPack;
using System;
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
                if (descriptionExist && (_wikidata.Lead.Description.Contains("filmaker") || _wikidata.Lead.Description.Contains("actor")|| _wikidata.Lead.Description.Contains("actress")))
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

            // Set actual result
            switch (_resultType)
            {
                // Set movie star property
                case ResultType.MovieStar:

                    // Get Tmdb information
                    string movieStarName = _wikidata.Lead.Displaytitle;

                    // Get the client
                    TMDbClient client = new TMDbClient(App.Current.Resources["TmdbKey"].ToString());

                    // Get a collection from the research
                    SearchContainer<SearchPerson> collectons = client.SearchPersonAsync(movieStarName).Result;

                    // Get person information
                    var movieStar = collectons.Results.First();
                    MovieCredits movieStarCredit = client.GetPersonMovieCreditsAsync(movieStar.Id).Result;
                    _actualResult = new MovieStarResult()
                    {
                        Img = _wikidata.Lead.Image.Urls["640"].ToString(),
                        Title = movieStar.Name,
                        Description = _wikidata.Lead.Description,
                        CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")?[0].InnerText,
                        Age = (DateTime.Now.Year - DateTime.Parse(birthdate).Year).ToString(),
                        Birthdate = DateTime.Parse(birthdate).ToString("MMMM dd, yyyy"),
                        Birthplace = documentNode.SelectNodes("//*[@class='birthplace']")?[0].InnerText,
                        Deathplace = documentNode.SelectNodes("//*[@class='deathplace']")?[0].InnerText,
                        Residence = documentNode.SelectNodes("//*[@class='label']")?[0].InnerText,
                        Type = _resultType,
                        KnownFor = movieStar.KnownFor,
                        SeenOn = movieStarCredit.Cast.OrderBy(movie => movie.ReleaseDate).Reverse().ToList<MovieRole>(),

                    };
                    break;
                // Set movie star property
                case ResultType.Movie:

                    // Get Tmdb information
                    string movieTitle = WkeToolbox.HtmlEscape(_wikidata.Lead.Displaytitle);

                    // Get the client
                    TMDbClient clientMovie = new TMDbClient(App.Current.Resources["TmdbKey"].ToString());

                    // Get a collection from the research
                    SearchContainer<SearchMovie> movieInfo = clientMovie.SearchMovieAsync(movieTitle).Result;

                    // Get person information
                    var movie = movieInfo.Results.First();

                    var movieCredit = clientMovie.GetMovieCreditsAsync(movie.Id).Result;
                    //var movieGenre = clientMovie.GetGenreMoviesAsync(movie.Id).Result;
                    _actualResult = new MovieResult()
                    {
                        Img = "https://image.tmdb.org/t/p/w600_and_h900_bestv2" + movie.PosterPath,
                        Title = movie.Title,
                        Description = movie.Overview,
                        Casts = movieCredit.Cast,
                        Rate = movie.VoteAverage.ToString() + "/10",
                        ReleaseDate = movie.ReleaseDate.Value,
                        Type = _resultType,
                    };
                    break;

                // Set Person property
                case ResultType.Person:
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image?.Urls["640"].ToString(),
                        Title = _wikidata.Lead.Displaytitle,
                        Description = _wikidata.Lead.Description,
                        CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")?[0].InnerText,
                        Age = (DateTime.Now.Year - DateTime.Parse(birthdate).Year).ToString(),
                        Birthdate = DateTime.Parse(birthdate).ToString("MMMM dd, yyyy"),
                        Birthplace = documentNode.SelectNodes("//*[@class='birthplace']")?[0].InnerText,
                        Deathplace = documentNode.SelectNodes("//*[@class='deathplace']")?[0].InnerText,
                        Residence = documentNode.SelectNodes("//*[@class='label']")?[0].InnerText,
                        Type = _resultType,

                    };
                    break;
                case ResultType.Other:
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image?.Urls["640"].ToString(),
                        Title = _wikidata.Lead.Displaytitle,
                        Description = _wikidata.Lead.Description,
                        Type = _resultType,
                        ExtendedDescription = htmlDoc.DocumentNode.InnerText,

                    };
                    break;
                // Otherwise... to avoid crashes...
                default:
                    _actualResult = new SearchResult();
                    break;
            }
            return _actualResult;
        }
    }
}
