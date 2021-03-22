using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StreetASP.Models
{
    public class CreateViewModel
    {
        [StringLength(200)]
        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Street name must not be Null")]
        public string StreetName { get; set; }

        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "Country name must not be Null")]
        public string CountryName { get; set; }

        
    }
}