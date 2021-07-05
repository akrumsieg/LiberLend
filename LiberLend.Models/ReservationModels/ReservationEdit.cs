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
        [DataType(DataType.Date)]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        [Display(Name = "Reservation End")]
        [DataType(DataType.Date)]
        public DateTimeOffset EndTime { get; set; }
    }
}
