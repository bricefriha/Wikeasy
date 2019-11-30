using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Wikeasy.Models
{
    public class WikiData
    {
        [JsonProperty(PropertyName = "displaytitle")]
        public string displaytitle { get; set; }
        [JsonProperty(PropertyName = "normalizedtitle")]
        public string normalizedtitle { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "image")]
        public WikiImage image { get; set; }
        //[JsonProperty(PropertyName = "sections")]
        //public IDictionary<string, Match> sections { get; set; }
        //[JsonProperty(PropertyName = "remaining")]
        //public IDictionary<string, Match> remaining { get; set; }
    }
}
