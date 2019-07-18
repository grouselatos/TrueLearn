using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class Friend
    {
        [Key]
        [Required]
        public int id { get; set; }

        public virtual ApplicationUser requested_from_id { get; set; }
        public virtual ApplicationUser requested_to_id { get; set; }

        public Enums.Status status { get; set; }
    }
}