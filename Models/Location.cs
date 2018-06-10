using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Character has exceeded maximum limit")]
        [Display(Name = "Location")]
        public string LocationName { get; set; }
    }
}