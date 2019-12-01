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

        [JsonProperty(PropertyName = "lead")]
        public WikiDataLead Lead { get; set; }
        //[JsonProperty(PropertyName = "remaining")]
        //public IDictionary<string, Match> Remaining { get; set; }
    }
    public class WikiDataLead
    {
        [JsonProperty(PropertyName = "displaytitle")]
        public string Displaytitle { get; set; }
        [JsonProperty(PropertyName = "normalizedtitle")]
        public string Normalizedtitle { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "image")]
        public WikiDataImage Image { get; set; }
        [JsonProperty(PropertyName = "sections")]
        public WikiDataSection[] Sections { get; set; }


    }
    
    public class WikiDataSection
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "toclevel")]
        public string Toclevel { get; set; }
        [JsonProperty(PropertyName = "anchor")]
        public string Anchor { get; set; }
        [JsonProperty(PropertyName = "line")]
        public string Line { get; set; }


    }
    public class WikiDataImage
    {
        [JsonProperty(PropertyName = "file")]
        public string File { get; set; }
        [JsonProperty(PropertyName = "urls")]
        public Dictionary<string, string> Urls { get; set; }
    }

}
