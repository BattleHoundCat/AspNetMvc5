using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StreetASP.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Country Name")]
        public string Name { get; set; }
        public virtual ICollection<Street> Streets { get; set; }
    }
}