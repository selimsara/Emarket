using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emarket.Models
{
    public class Category
    {
        [Key]

        public int ID { get; set; }
        [Required]
        [Display(Name = "Category")]
        
        public string Name { get; set; }
        [Display(Name = "Number of Products")]
        public int Number_of_products { get; set; }
        public virtual ICollection<Products> Products { get; set; } 
    }
}