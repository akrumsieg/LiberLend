using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.ReservationModels
{
    public class ReservationDetails
    {
        public int ReservationId { get; set; }

        [Display(Name = "Book Owner Name")]
        public string OwnerName { get; set; }

        [Display(Name = "Book Owner Email")]
        public string OwnerEmail { get; set; }

        [Display(Name = "Book Owner Phone")]
        public string OwnerPhone { get; set; }

        [Display(Name = "Borrower Name")]
        public string BorrowerName { get; set; }

        [Display(Name = "Borrower Email")]
        public string BorrowerEmail { get; set; }

        [Display(Name = "Borrower Phone")]
        public string BorrowerPhone { get; set; }

        public int BookId { get; set; }

        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [Display(Name = "Book Author")]
        public string BookAuthor { get; set; }

        [Display(Name = "Reservation Period")]
        public string ReservationPeriod { get; set; }



    }
}
