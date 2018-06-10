using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintID { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string ComplaintBox { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime ComplaintDate { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [ForeignKey("ComplaintCategory")]
        public int ComplaintCatId { get; set; }

        //[ForeignKey("UserProfile")]
        //public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        //Navigational Properties
        public virtual ComplaintCategory ComplaintCategory { get; set; }
        public virtual Product Product { get; set; }
        public virtual Location Location { get; set; }
        //public virtual UserProfile UserProfile { get; set; }
    }
}