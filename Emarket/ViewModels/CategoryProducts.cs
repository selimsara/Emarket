using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Emarket.Models;

namespace Emarket.ViewModels
{
    public class CategoryProducts
    {
        public Products Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}