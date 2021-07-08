using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.ReservationModels
{
    public class ReservationCreate
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Display(Name = "Reservation Start")]
        //[DataType(DataType.Date)]
        public string StartTime { get; set; }

        [Required]
        [Display(Name = "Reservation End")]
        //[DataType(DataType.Date)]
        public string EndTime { get; set; }

        public List<string> ReservedDates { get; set; }
    }
}
