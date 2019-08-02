using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    [CustomAuthorize(Roles = "PremiumUser")]
    public class SuggestionDummy
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}