﻿using LiberLend.Data;
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
                Genre = model.Genre
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
                var query = ctx.Books.AsEnumerable().Where(b => b.ApplicationUserId == _userId)
                            .Select(b => new BookListItem
                            {
                                BookId = b.BookId,
                                ApplicationUserId = b.ApplicationUserId,
                                Title = b.Title,
                                Author = b.AuthorFullNameFL(),
                                IsAvailable = b.IsAvailable
                            });
                return query.ToArray();
            }
        }

        public BookDetails GetBookById(int id, int? libraryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool userIsMember;
                try
                {
                    var members = ctx.Libraries.AsEnumerable().Single(l => l.LibraryId == libraryId).Memberships.Select(m => m.ApplicationUserId);
                    userIsMember = members.Contains(_userId);
                }
                catch
                {
                    userIsMember = false;
                }
                var entity = ctx.Books.Single(b => b.BookId == id);
                return new BookDetails
                {
                    BookId = entity.BookId,
                    ApplicationUserId = entity.ApplicationUserId,
                    UserIsMember = userIsMember,
                    ISBN = entity.ISBN,
                    Title = entity.Title,
                    Author = entity.AuthorFullNameFL(),
                    AuthorFirstName = entity.AuthorFirstName,
                    AuthorLastName = entity.AuthorLastName,
                    Publisher = entity.Publisher,
                    Description = entity.Description,
                    Edition = entity.Edition,
                    Genre = entity.Genre,
                    IsAvailable = entity.IsAvailable
                };
            }
        }

        public bool EditBook(BookEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Single(b => b.ApplicationUserId == _userId && b.BookId == model.BookId);
                entity.ISBN = model.ISBN;
                entity.Title = model.Title;
                entity.AuthorFirstName = model.AuthorFirstName;
                entity.AuthorLastName = model.AuthorLastName;
                entity.Publisher = model.Publisher;
                entity.Description = model.Description;
                entity.Edition = model.Edition;
                entity.Genre = model.Genre;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Single(b => b.ApplicationUserId == _userId && b.BookId == id);
                ctx.Books.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
