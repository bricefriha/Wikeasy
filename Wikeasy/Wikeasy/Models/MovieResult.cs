using System;
using System.Collections.Generic;
using System.Text;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Wikeasy.Models
{
    public class MovieResult : MovieContentResult
    {
        public string Overview { get; set; }
        public string Rate { get; set; }
        public List<Cast> Casts { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
