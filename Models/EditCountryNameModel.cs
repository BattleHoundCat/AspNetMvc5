using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StreetASP.Models
{
    public class EditCountryNameModel
    {
        public int StreetId { get; set; }

        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "Country name must not be Null")]
        public string CountryName { get; set; }
        public string StreetName { get; set; }
    }
}