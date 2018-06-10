using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product {0} is too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a value for {0}")]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal),"1000.00", "9999999.00", ErrorMessage = "{0} specified is not within range")]
        public decimal Price { get; set; }

        [MaxLength(600)]
        public string PictureUrl { get; set; }

        [DataType(dataType: DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Manufactured")]
        public DateTime ManufacturedDate { get; set; }

        [DataType(dataType: DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [DataType(dataType: DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        //Foreign Key
        [ForeignKey("ProductCategory")]
        public int ProductCatId { get; set; }

        //Navigation Property
        public virtual ProductCategory ProductCategory { get; set; }
    }
}