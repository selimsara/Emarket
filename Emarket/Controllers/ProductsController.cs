
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emarket.Context;
using System.Data.Entity;
using Emarket.ViewModels;
using System.Collections.Generic;
using Emarket.Models;

namespace Emarket.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(x => x.Category).ToList();
            return View(products);
        }
        [HttpPost]

        public ActionResult Index(string Search)
        {
            List<Products> products;
            if (string.IsNullOrEmpty(Search))
            {
                products = db.Products.Include(s => s.Category).ToList();
            }
            else
            {
                products = db.Products.Include(s => s.Category).Where(x => x.Category.Name.ToLower().StartsWith(Search.ToLower())).ToList();
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var category = db.Category.ToList();
            CategoryProducts categoryProducts = new CategoryProducts
            {
                Categories = category,
            };
            return View(categoryProducts);
        }

        [HttpPost]
        public ActionResult Create(CategoryProducts categoryProducts, HttpPostedFileBase imgfile)
        {
            if(ModelState.IsValid)
            {
                if (imgfile != null)
                {
                    string photo = System.IO.Path.GetFileName(imgfile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/" ), photo);
                    imgfile.SaveAs(path);
                    categoryProducts.Products.Image = photo;  

;                }

                db.Products.Add(categoryProducts.Products);
                db.SaveChanges();


                var category = db.Category.SingleOrDefault(x => x.ID == categoryProducts.Products.Id);
                category.Number_of_products++;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.SingleOrDefault(i => i.Id == id);  
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = db.Products.SingleOrDefault(s => s.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var category = db.Category.ToList();
            CategoryProducts categoryProducts = new CategoryProducts
            {
                Products = product,
                Categories = category,
            };
            return View(categoryProducts);
        }
        

        [HttpPost]
        public ActionResult Edit(CategoryProducts categoryProducts, HttpPostedFileBase imgfile)
        {
            if(ModelState.IsValid)
            {
                var product = db.Products.SingleOrDefault(s => s.Id == categoryProducts.Products.Id);
                product.Name = categoryProducts.Products.Name;
                product.Description = categoryProducts.Products.Description;
                product.Price = categoryProducts.Products.Price;
                product.Category_id = categoryProducts.Products.Category_id;

                if (imgfile != null)
                {
                    string photo = System.IO.Path.GetFileName(imgfile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/"), photo);
                    imgfile.SaveAs(path);
                    product.Image = photo;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = categoryProducts.Products.Id });

            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {

                var category = db.Category.SingleOrDefault(x => x.ID == id);
                category.Number_of_products--;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }

            
            return View("Index");
        }

    }
}