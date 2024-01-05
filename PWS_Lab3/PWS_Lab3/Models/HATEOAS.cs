﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWS_Lab3.Models
{
    public class HATEOAS
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public HATEOAS(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}