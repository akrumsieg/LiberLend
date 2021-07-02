using LiberLend.Data;
using LiberLend.Models.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Services
{
    public class LibraryService
    {
        private readonly string _userId;

        public LibraryService(string userId)
        {
            _userId = userId;
        }

        //When a user creates a new library, membership for that library will automatically be created as well
        public bool CreateLibrary(LibraryCreate model)
        {
            var library = new Library
            {
                ApplicationUserId = _userId,
                Name = model.Name,
                Description = model.Description
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Libraries.Add(library);
                var membership = new Membership
                {
                    LibraryId = library.LibraryId,
                    ApplicationUserId = _userId,
                    IsAuthorized = true
                };
                ctx.Memberships.Add(membership);
                return ctx.SaveChanges() == 2;
            }
        }

        //Gets ALL libraries in the db
        public IEnumerable<LibraryListItem> GetAllLibraries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Libraries.AsEnumerable()
                            .Select(l => new LibraryListItem
                            {
                                LibraryId = l.LibraryId,
                                ApplicationUserId = l.ApplicationUserId,
                                Name = l.Name,
                                Description = l.Description
                            });
                return query.ToArray();
            }
        }

        //Gets libraries where user is the caretaker
        public IEnumerable<LibraryListItem> GetCaretakerLibraries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Libraries.AsEnumerable().Where(l => l.ApplicationUserId == _userId)
                            .Select(l => new LibraryListItem
                            {
                                LibraryId = l.LibraryId,
                                ApplicationUserId = l.ApplicationUserId,
                                Name = l.Name,
                                Description = l.Description
                            });
                return query.ToArray();
            }
        }

        //Gets libraries where user is a member (includes libraries where user is the caretaker)
        public IEnumerable<LibraryListItem> GetMemberLibraries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Memberships.Where(m => m.ApplicationUserId == _userId)
                            .Select(m => new LibraryListItem
                            {
                                LibraryId = m.Library.LibraryId,
                                ApplicationUserId = m.Library.ApplicationUserId,
                                Name = m.Library.Name,
                                Description = m.Library.Description
                            });
                return query.ToArray();
            }
        }

        public LibraryDetails GetLibraryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Libraries.Single(l => l.LibraryId == id);
                return new LibraryDetails
                {
                    LibraryId = entity.LibraryId,
                    ApplicationUserId = entity.ApplicationUserId,
                    Name = entity.Name,
                    Description = entity.Description,
                    CaretakerName = entity.ApplicationUser.FullNameFL(),
                    NumOfMembers = 1, // entity.Memberships.Count(),
                    NumOfBooks = 0 //entity. ???
                };
            }
        }

        public bool EditLibrary(LibraryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Libraries.Single(l => l.ApplicationUserId == _userId && l.LibraryId == model.LibraryId);
                entity.Name = model.Name;
                entity.Description = model.Description;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLibrary(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Libraries.Single(l => l.ApplicationUserId == _userId && l.LibraryId == id);
                ctx.Libraries.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
