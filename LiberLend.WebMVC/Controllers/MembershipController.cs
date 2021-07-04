using LiberLend.Models.MembershipModels;
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
    public class MembershipController : Controller
    {
        private MembershipService CreateMembershipService()
        {
            var userId = User.Identity.GetUserId();
            return new MembershipService(userId);
        }

        // GET: Membership
        public ActionResult Index()
        {
            var service = CreateMembershipService();
            return View(service.GetAllMemberships());
        }

        //GET: Details
        public ActionResult Details(int id)
        {
            var service = CreateMembershipService();
            return View(service.GetMembershipById(id));
        }

        //GET: Membership list by library id
        public ActionResult LibraryMembers(int id)
        {
            var service = CreateMembershipService();
            return View(service.GetMembershipsByLibraryId(id));
        }

        //GET: Book list by library id
        public ActionResult LibraryBooks(int id)
        {
            var service = CreateMembershipService();
            return View(service.GetBooksByLibraryId(id));
        }

        //GET: Create
        public ActionResult Create(int id)
        {
            var model = new MembershipCreate
            {
                LibraryId = id,
            };
            return View(model);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembershipCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateMembershipService();
            if (service.CreateMembership(model))
            {
                TempData["SaveResult"] = "Membership was successfully created!";
                return RedirectToAction("../Library/Index");
            }
            ModelState.AddModelError("", "Membership could not be created.");
            return View(model);
        }

        //Get: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateMembershipService();
            var detail = service.GetMembershipById(id);
            var model = new MembershipEdit
            {
                MembershipId = detail.MembershipId
            };
            return View(model);
        }

        //POST: Edit
        //The library caretaker uses this function to toggle IsAuthorized
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MembershipEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.MembershipId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateMembershipService();
            if (service.EditMembership(model))
            {
                TempData["SaveResult"] = "Update successful!";
                return RedirectToAction("../Library/Index");
            }
            ModelState.AddModelError("", "Update failed.");
            return View(model);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateMembershipService();
            return View(service.GetMembershipById(id));
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateMembershipService();
            if (service.DeleteMembership(id))
            {
                TempData["SaveResult"] = "This membership was deleted.";
                return RedirectToAction("../Library/Index");
            }
            TempData["SaveResult"] = "Delete failed.";
            return RedirectToAction("../Library/Index");
        }
    }
}