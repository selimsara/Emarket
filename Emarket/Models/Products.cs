using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Emarket.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You have to Enter the Product's name")]
        
        
        public string Name { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Category")]
        [Required]

        public virtual Category Category { get; set; }
        [Display(Name = "Price")]
        [Required]
        public float Price { get; set; }
        [ForeignKey("Category")]
        public int Category_id { get; set; }

    }
}