using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.MembershipModels
{
    public class MembershipCreate
    {
        [Required]
        public int LibraryId { get; set; }
    }
}
