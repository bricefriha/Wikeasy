using System;
using System.Collections.Generic;
using System.Text;
using TMDbLib.Objects.Search;

namespace Wikeasy.Models
{
    class MovieStarResult : SearchResult
    {
        public List<KnownForBase> KnownFor { get; set; }
    }
}
