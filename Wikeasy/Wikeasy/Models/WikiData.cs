using System;
using System.Collections.Generic;
using System.Text;

namespace Wikeasy.Models
{
    public class WikiData
    {
        public string displaytitle { get; set; }
        public string normalizedtitle { get; set; }
        public string description { get; set; }
        public Dictionary<string, Dictionary<string, string>> image { get; set; }
        public List<Dictionary<string, string>> sections { get; set; }
        public List<Dictionary<string, string>> remaining { get; set; }
    }
}
