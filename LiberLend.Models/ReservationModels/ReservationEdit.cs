using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.ReservationModels
{
    public class ReservationEdit
    {
        public int ReservationId { get; set; }

        [Required]
        [Display(Name = "Reservation Start")]
        public string StartTime { get; set; }

        [Required]
        [Display(Name = "Reservation End")]
        public string EndTime { get; set; }

        public List<string> ReservedDates { get; set; }

        public List<string> BlackoutDates { get; set; }
    }
}
