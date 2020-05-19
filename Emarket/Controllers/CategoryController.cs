using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emarket.Context;
using Emarket.Models;

namespace Emarket.Controllers
{
    public class CategoryController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Category
        public ActionResult Index()
        {
            var category = db.Category.ToList();
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

    }
}