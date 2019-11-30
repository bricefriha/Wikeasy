using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wikeasy.Models
{
    public class WikiImage
    {
        [JsonProperty(PropertyName = "file")]
        public string file { get; set; }
        [JsonProperty(PropertyName = "urls")]
        public Dictionary<string, string> urls { get; set; }
    }
}
