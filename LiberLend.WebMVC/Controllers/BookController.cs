﻿using LiberLend.Models.BookModels;
using LiberLend.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiberLend.WebMVC.Controllers
{
    [Authorize]
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

        //GET: Details
        public ActionResult Details(int id, int? libraryId)
        {
            var service = CreateBookService();
            return View(service.GetBookById(id, libraryId));
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
                TempData["SaveResult"] = "You put a book on your bookshelf!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your book could not be added.");
            return View(model);
        }

        //GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateBookService();
            var detail = service.GetBookById(id, null);
            var model = new BookEdit
            {
                BookId = detail.BookId,
                ISBN = detail.ISBN,
                Title = detail.Title,
                AuthorFirstName = detail.AuthorFirstName,
                AuthorLastName = detail.AuthorLastName,
                Publisher = detail.Publisher,
                Description = detail.Description,
                Edition = detail.Edition,
                Genre = detail.Genre
            };
            return View(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if(model.BookId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateBookService();
            if (service.EditBook(model))
            {
                TempData["SaveResult"] = "Your book was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your book could not be updated. Did you change any information?");
            return View(model);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateBookService();
            return View(service.GetBookById(id, null));
        }

        //POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBookService();
            if (service.DeleteBook(id))
            {
                TempData["SaveResult"] = "Your book was deleted.";
                return RedirectToAction("Index");
            }
            TempData["SaveResult"] = "Your book could not be deleted.";
            return RedirectToAction("Index");
        }
    }
}