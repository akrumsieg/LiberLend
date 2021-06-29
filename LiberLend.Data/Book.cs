using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        public string Edition { get; set; }

        public string Genre { get; set; }

        public bool IsAvailable { get; set; }

        //virtual List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public string FullNameFL() => AuthorFirstName + AuthorLastName;

        public string FullNameLF() => AuthorLastName + ", " + AuthorFirstName;
    }
}
