using LiberLend.Data;
using LiberLend.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Services
{
    public class BookService
    {
        private readonly string _userId;

        public BookService(string userId)
        {
            _userId = userId;
        }

        public bool CreateBook(BookCreate model)
        {
            var entity = new Book
            {
                ApplicationUserId = _userId,
                ISBN = model.ISBN,
                Title = model.Title,
                AuthorFirstName = model.AuthorFirstName,
                AuthorLastName = model.AuthorLastName,
                Publisher = model.Publisher,
                Description = model.Description,
                Edition = model.Edition,
                Genre = model.Genre,
                IsAvailable = true
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookListItem> GetAllBooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Books.Where(b => b.ApplicationUserId == _userId)
                            .Select(b => new BookListItem
                            {
                                Title = b.Title,
                                Author = b.FullNameFL(),
                                IsAvailable = b.IsAvailable
                            });
                return query.ToArray();
            }
        }
    }
}
