using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace StreetASP.Models
{
    public class Street
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Street Name")]
        public string Name { get; set; }
        public Country Country { get; set; }

       // public int CountryId { get; set; }
    }
}