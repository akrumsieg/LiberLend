using LiberLend.Models.BookModels;
using LiberLend.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiberLend.WebMVC.Controllers
{
    public class BookController : Controller
    {
        private BookService CreateBookService()
        {
            var userId = User.Identity.GetUserId();
            return new BookService(userId);
        }

        // GET: Book
        public ActionResult Index()
        {
            var service = CreateBookService();
            return View(service.GetAllBooks());
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateBookService();
            if (service.CreateBook(model))
            {
                TempData["SaveResult"] = "Your book was entered in your personal library.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your book could not be added.");
            return View(model);
        }
    }
}