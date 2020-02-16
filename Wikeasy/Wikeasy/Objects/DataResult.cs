using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Wikeasy.Models;

namespace Wikeasy.Objects
{
    public class DataResult
    {
        // Attributs
        private ResultType _resultType;
        private WikiData _wikidata;
        private SearchResult _actualResult;

        public DataResult(WikiData wikidata)
        {
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
            // ToDo: I feel there is a way to optimise all these stuffs
            if (birthdate != null)
            {
                // it's a movie star
                if (_wikidata.Lead.Description.Contains("filmaker") || _wikidata.Lead.Description.Contains("actor"))
                    _resultType = ResultType.MovieStar;

                // other wise
                else
                    _resultType = ResultType.Person;
            }
            else
            {
                // is a movie
                if (_wikidata.Lead.Description.Contains("film") || _wikidata.Lead.Description.Contains("movie"))
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
            string birthdate = documentNode.SelectNodes("//*[@class='bday']") is null ? null : documentNode.SelectNodes("//*[@class='bday']")[0].InnerText;

            // Set result type
            this.SetResultType(birthdate);

            // Set actual result
            switch (_resultType)
            {
                // Set movie star property
                case ResultType.MovieStar:
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image.Urls["640"].ToString(),
                        Title = _wikidata.Lead.Displaytitle,
                        Description = _wikidata.Lead.Description,
                        CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']") is null ? null : documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")[0].InnerText,
                        Age = (DateTime.Now.Year - DateTime.Parse(birthdate).Year).ToString(),
                        Birthdate = DateTime.Parse(birthdate).ToString("MMMM dd, yyyy"),
                        Birthplace = documentNode.SelectNodes("//*[@class='birthplace']") is null ? null : documentNode.SelectNodes("//*[@class='birthplace']")[0].InnerText,
                        Deathplace = documentNode.SelectNodes("//*[@class='deathplace']") is null ? null : documentNode.SelectNodes("//*[@class='deathplace']")[0].InnerText,
                        Residence = documentNode.SelectNodes("//*[@class='label']") is null ? null : documentNode.SelectNodes("//*[@class='label']")[0].InnerText,

                    };
                    // ToDo: Add iMDB data too
                    break;

                // Set movie star property
                case ResultType.Person:
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image.Urls["640"].ToString(),
                        Title = _wikidata.Lead.Displaytitle,
                        Description = _wikidata.Lead.Description,
                        CurrentActivity = documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']") is null ? null : documentNode.SelectNodes("//*[@class='shortdescription nomobile noexcerpt noprint searchaux']")[0].InnerText,
                        Age = (DateTime.Now.Year - DateTime.Parse(birthdate).Year).ToString(),
                        Birthdate = DateTime.Parse(birthdate).ToString("MMMM dd, yyyy"),
                        Birthplace = documentNode.SelectNodes("//*[@class='birthplace']") is null ? null : documentNode.SelectNodes("//*[@class='birthplace']")[0].InnerText,
                        Deathplace = documentNode.SelectNodes("//*[@class='deathplace']") is null ? null : documentNode.SelectNodes("//*[@class='deathplace']")[0].InnerText,
                        Residence = documentNode.SelectNodes("//*[@class='label']") is null ? null : documentNode.SelectNodes("//*[@class='label']")[0].InnerText,

                    };
                    break;
                case ResultType.Other:
                    _actualResult = new SearchResult()
                    {
                        Img = _wikidata.Lead.Image.Urls["640"].ToString(),
                        Title = _wikidata.Lead.Displaytitle,
                        Description = _wikidata.Lead.Description,

                    };
                    break;
                // Otherwise, to avoid crashes
                default:
                    _actualResult = new SearchResult();
                    break;
            }
            return _actualResult;
        }
    }
}
