using LiberLend.Models.ReservationModels;
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
    public class ReservationController : Controller
    {
        private ReservationService CreateReservationService()
        {
            var userId = User.Identity.GetUserId();
            return new ReservationService(userId);
        }

        //GET: Index view with links to other reservation index views
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reservations As Borrower
        public ActionResult IndexAsBorrower()
        {
            var service = CreateReservationService();
            return View(service.GetAllReservationsAsBorrower());
        }

        // GET: Reservations As Owner
        public ActionResult IndexAsOwner()
        {
            var service = CreateReservationService();
            return View(service.GetAllReservationsAsOwner());
        }

        //GET: Details
        public ActionResult Details(int id)
        {
            var service = CreateReservationService();
            return View(service.GetReservationById(id));
        }

        //GET: Create
        public ActionResult Create(int id)
        {
            var service = CreateReservationService();
            var reservedDates = service.GetReservedDatesForBookId(id);
            var model = new ReservationCreate
            {
                BookId = id,
                ReservedDates = reservedDates
            };
            return View(model);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateReservationService();
            if (service.CreateReservation(model))
            {
                TempData["SaveResult"] = "Reservation was successfully created!";
                return RedirectToAction("IndexAsBorrower");
            }
            ModelState.AddModelError("", "Reservation could not be created.");
            return View(model);
        }

        //Get: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateReservationService();
            var detail = service.GetReservationById(id);
            var reservedDates = service.GetReservedDatesForBookId(detail.BookId);
            var model = new ReservationEdit
            {
                ReservationId = detail.ReservationId,
                StartTime = detail.StartTime,
                EndTime = detail.EndTime,
                ReservedDates = reservedDates,
                BlackoutDates = reservedDates.Except(detail.CurrentReservationDates).ToList()
            };
            return View(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReservationEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ReservationId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateReservationService();
            if (service.EditReservation(model))
            {
                TempData["SaveResult"] = "Update successful!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Update failed. Did you change the dates?");
            return View(model);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateReservationService();
            return View(service.GetReservationById(id));
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateReservationService();
            if (service.DeleteReservation(id))
            {
                TempData["SaveResult"] = "This reservation was cancelled.";
                return RedirectToAction("IndexAsBorrower");
            }
            TempData["SaveResult"] = "Cancellation failed.";
            return RedirectToAction("IndexAsBorrower");
        }
    }
}