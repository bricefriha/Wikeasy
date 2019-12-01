using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wikeasy.Models;

namespace Wikeasy.Services
{
    public interface IWikipediaApiDataService
    {
        Task<WikiData> GetWikiData(string name);
    }
}
