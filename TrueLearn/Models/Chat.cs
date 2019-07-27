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
        [Key, ForeignKey("Friend")]
        public int Id { get; set; }
        public virtual Friend Friend { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}