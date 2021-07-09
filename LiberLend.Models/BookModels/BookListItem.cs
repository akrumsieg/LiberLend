using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.BookModels
{
    public class BookListItem
    {
        public int BookId { get; set; }

        public int? LibraryId { get; set; }

        public bool UserIsMember { get; set; }

        public string ApplicationUserId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
    }
}
