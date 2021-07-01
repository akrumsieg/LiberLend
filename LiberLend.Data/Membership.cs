using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Data
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        [ForeignKey(nameof(Library))]
        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool IsAuthorized { get; set; }
    }
}
