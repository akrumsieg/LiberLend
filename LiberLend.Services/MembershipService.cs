using LiberLend.Data;
using LiberLend.Models.BookModels;
using LiberLend.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Services
{
    public class MembershipService
    {
        private readonly string _userId;

        public MembershipService(string userId)
        {
            _userId = userId;
        }

        public bool CreateMembership(MembershipCreate model)
        {
            var entity = new Membership
            {
                ApplicationUserId = _userId,
                LibraryId = model.LibraryId,
                IsAuthorized = true //if functionality is built for membership approval by library caretaker, this should be false
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Memberships.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MembershipListItem> GetAllMemberships()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Memberships.AsEnumerable().Where(m => m.ApplicationUserId == _userId)
                            .Select(m => new MembershipListItem
                            {
                                MembershipId = m.MembershipId,
                                ApplicationUserName = m.ApplicationUser.FullNameFL(),
                                IsAuthorized = m.IsAuthorized
                            });
                return query.ToArray();
            }
        }

        public IEnumerable<MembershipListItem> GetMembershipsByLibraryId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Memberships.AsEnumerable().Where(m => m.LibraryId == id)
                                .Select(m => new MembershipListItem
                                {
                                    MembershipId = m.MembershipId,
                                    ApplicationUserName = m.ApplicationUser.FullNameFL(),
                                    IsAuthorized = m.IsAuthorized
                                });
                return query.ToArray();
            }
        }

        public IEnumerable<BookListItem> GetBooksByLibraryId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Memberships.AsEnumerable()
                            .Where(m => m.LibraryId == id)
                            .Select(m => m.ApplicationUser.Books)
                            .ToList();

                List<BookListItem> books = new List<BookListItem>();
                foreach (var bookList in query)
                {
                    foreach (var book in bookList)
                    {
                        var bli = new BookListItem
                        {
                            BookId = book.BookId,
                            Title = book.Title,
                            Author = book.FullNameLF(),
                            IsAvailable = book.IsAvailable
                        };
                        books.Add(bli);
                    }
                }
                return books.OrderBy(b => b.Author);
            }
        }

        public MembershipDetails GetMembershipById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Memberships.Single(m => m.MembershipId == id);
                return new MembershipDetails
                {
                    MembershipId = entity.MembershipId,
                    LibraryName = entity.Library.Name,
                    ApplicationUserName = entity.ApplicationUser.FullNameFL(),
                    IsAuthorized = entity.IsAuthorized
                };
            }
        }

        //The creator of the membership should not edit (authorize) the membership. Only the library caretaker should edit (authorize) it.
        public bool EditMembership(MembershipEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Memberships.Single(m => m.MembershipId == model.MembershipId && m.Library.ApplicationUserId == _userId);
                entity.IsAuthorized = !entity.IsAuthorized;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMembership(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Memberships.Single(m => m.ApplicationUserId == _userId && m.MembershipId == id);
                ctx.Memberships.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
