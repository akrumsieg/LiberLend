using LiberLend.Data;
using LiberLend.Models.ReservationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Services
{
    public class ReservationService
    {
        private readonly string _userId;

        public ReservationService(string userId)
        {
            _userId = userId;
        }

        public bool CreateReservation(ReservationCreate model)
        {
            var entity = new Reservation
            {
                ApplicationUserId = _userId,
                BookId = model.BookId,
                StartTime = DateTime.Parse(model.StartTime),
                EndTime = DateTime.Parse(model.EndTime)
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Reservations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReservationListItem> GetAllReservationsAsBorrower()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Users.Single(u => u.Id == _userId);
                var reservations = new List<ReservationListItem>();
                foreach (Reservation r in entity.Reservations)
                {
                    reservations.Add(new ReservationListItem
                    {
                        ReservationId = r.ReservationId,
                        OwnerName = r.Book.ApplicationUser.FullNameFL(),
                        BorrowerName = r.ApplicationUser.FullNameFL(),
                        BookTitle = r.Book.Title,
                        BookAuthor = r.Book.AuthorFullNameFL(),
                        ReservationPeriod = r.StartTime.ToString("d") + " - " + r.EndTime.ToString("d")
                    });
                }
                return reservations.OrderBy(r => r.ReservationPeriod);

                //var query = ctx.Reservations.AsEnumerable().Where(r => r.ApplicationUserId == _userId)
                //            .Select(r => new ReservationListItem
                //            {
                //                ReservationId = r.ReservationId,
                //                OwnerName = r.Book.ApplicationUser.FullNameFL(),
                //                BorrowerName = r.ApplicationUser.FullNameFL(),
                //                BookTitle = r.Book.Title,
                //                BookAuthor = r.Book.AuthorFullNameFL(),
                //                ReservationPeriod = r.StartTime.ToString() + " - " + r.EndTime.ToString() 
                //            });
                //return query.OrderBy(r => r.ReservationPeriod).ToArray();
            }
        }

        public IEnumerable<ReservationListItem> GetAllReservationsAsOwner()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Reservations.AsEnumerable().Where(r => r.Book.ApplicationUserId == _userId)
                            .Select(r => new ReservationListItem
                            {
                                ReservationId = r.ReservationId,
                                OwnerName = r.Book.ApplicationUser.FullNameFL(),
                                BorrowerName = r.ApplicationUser.FullNameFL(),
                                BookTitle = r.Book.Title,
                                BookAuthor = r.Book.AuthorFullNameFL(),
                                ReservationPeriod = r.StartTime.ToString("d") + " - " + r.EndTime.ToString("d")
                            });
                return query.OrderBy(r => r.ReservationPeriod).ToArray();
            }
        }

        public ReservationDetails GetReservationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Reservations.Single(r => r.ReservationId == id);
                return new ReservationDetails
                {
                    ReservationId = entity.ReservationId,
                    OwnerName = entity.Book.ApplicationUser.FullNameFL(),
                    OwnerEmail = entity.Book.ApplicationUser.Email,
                    OwnerPhone = entity.Book.ApplicationUser.PhoneNumber,
                    BorrowerName = entity.ApplicationUser.FullNameFL(),
                    BorrowerEmail = entity.ApplicationUser.Email,
                    BorrowerPhone = entity.ApplicationUser.PhoneNumber,
                    BookId = entity.BookId,
                    ISBN = entity.Book.ISBN,
                    BookTitle = entity.Book.Title,
                    BookAuthor = entity.Book.AuthorFullNameFL(),
                    ReservationPeriod = entity.StartTime.ToString("d") + " - " + entity.EndTime.ToString("d")
                };
            }
        }

        //allows the book owner AND the borrower to edit the reservation period
        public bool EditReservation(ReservationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Reservations.Single(r => r.ReservationId == model.ReservationId && (r.ApplicationUserId == _userId || r.Book.ApplicationUserId == _userId));
                entity.StartTime = model.StartTime;
                entity.EndTime = model.EndTime;
                return ctx.SaveChanges() == 1;
            }
        }

        //only the borrower will be able to delete their reservation
        public bool DeleteReservation(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Reservations.Single(r => r.ApplicationUserId == _userId && r.ReservationId == id);
                ctx.Reservations.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
