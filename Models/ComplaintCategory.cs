using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCNE_Cake.Models
{
    public class ComplaintCategory
    {
        [Key]
        public int ComplaintCatId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Remote("CheckComplaintCat", "ComplaintCat")] //TODO: Execute in the Complaint Category Controller
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} is too long")]
        [Display(Name = "Category")]
        public string ComplaintCatName { get; set; }

        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}