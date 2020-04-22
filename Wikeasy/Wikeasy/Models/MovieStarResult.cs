using System;
using System.Collections.Generic;
using System.Text;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;

namespace Wikeasy.Models
{
    class MovieStarResult : MovieContentResult
    {
        public List<KnownForBase> KnownFor { get; set; }
        public List<MovieRole> SeenOn { get; set; }
    }
}
