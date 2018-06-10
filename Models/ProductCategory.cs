using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCNE_Cake.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCatId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Category")]
        [Remote("CheckProductCat", "ProductCat")] //TODO : Execute in the Product Category Controller
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} is too long")]
        public string ProductCat { get; set; }

        //Navigation Properties
        public virtual ICollection<Product> Products { get; set; }
    }
}