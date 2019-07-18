using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class Chat
    {
        [Key]
        [ForeignKey("Friend")]
        public int friends_id { get; set; }
        public virtual Friend  Friend { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int sender_id { get; set; }
        public string transcript { get; set; }
    }
}