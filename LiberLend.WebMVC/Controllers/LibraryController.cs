using LiberLend.Models.LibraryModels;
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
    public class LibraryController : Controller
    {
        private LibraryService CreateLibraryService()
        {
            var userId = User.Identity.GetUserId();
            return new LibraryService(userId);
        }

        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        // GET: Library
        public ActionResult Index()
        {
            var service = CreateLibraryService();
            return View(service.GetMemberLibraries());
        }

        //GET: Details
        public ActionResult Details(int id)
        {
            var service = CreateLibraryService();
            return View(service.GetLibraryById(id));
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibraryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateLibraryService();
            if (service.CreateLibrary(model))
            {
                TempData["SaveResult"] = "You started a community library!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The community library could not be created.");
            return View(model);
        }

        //Get: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateLibraryService();
            var detail = service.GetLibraryById(id);
            var model = new LibraryEdit
            {
                LibraryId = detail.LibraryId,
                ApplicationUserId = detail.ApplicationUserId,
                Name = detail.Name,
                Description = detail.Description
            };
            return View(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LibraryEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.LibraryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateLibraryService();
            if (service.EditLibrary(model))
            {
                TempData["SaveResult"] = "Update successful!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Update failed.");
            return View(model);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateLibraryService();
            return View(service.GetLibraryById(id));
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateLibraryService();
            if (service.DeleteLibrary(id))
            {
                TempData["SaveResult"] = "This community library was deleted.";
                return RedirectToAction("Index");
            }
            TempData["SaveResult"] = "Delete failed.";
            return RedirectToAction("Index");
        }
    }
}