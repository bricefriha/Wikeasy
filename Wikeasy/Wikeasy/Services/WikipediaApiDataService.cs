using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wikeasy.Models;

namespace Wikeasy.Services
{
    public class WikipediaApiDataService : BaseHttpService, IWikipediaApiDataService
    {
        readonly Uri baseUri;
        readonly IDictionary<string, string> headers;

        public WikipediaApiDataService()
        {
            this.baseUri = new Uri("https://en.wikipedia.org/api/rest_v1/");
            this.headers = new Dictionary<string, string>();


        }
        public async Task<WikiData> GetWikiData(string name)
        {
            var url = new Uri(this.baseUri, string.Format("/page/mobile-sections/{0}", name));
            var response = await SendRequestAsync<WikiData>(url, HttpMethod.Get, this.headers);

            return response;
        }
    }
}
