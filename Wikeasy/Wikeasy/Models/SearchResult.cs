﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Wikeasy.Models
{
    public class SearchResult
    {
        public string Img { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CurrentActivity { get; set; }
        public string Activities { get; set; }
        public string Age { get; set; }
        public string Birthdate { get; set; }
        public string Deathdate { get; set; }
        public string Birthplace { get; set; }
        public string Deathplace { get; set; }
        public string Residence { get; set; }
        public ResultType Type { get; set; }
        public string ExtendedDescription { get; set; }
        public string WikipediaLink { get; set; }
        public Collection<string> AmbiguitySolutions { get; set; }
    }


}
