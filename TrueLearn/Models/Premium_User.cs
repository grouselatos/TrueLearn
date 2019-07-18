using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    [Table("premium_users")]
    public class Premium_User: ApplicationUser
    {
        public ICollection<Course> suggested_courses { get; set;}
        public ICollection<byte> certificates { get; set; }
    }
}