using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StreetASP.Models
{
    public class EditStreetNameModel
    {
        public int StreetId { get; set; }
        [StringLength(200)]
        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Street name must not be Null")]
        public string StreetName { get; set; }

    }
}