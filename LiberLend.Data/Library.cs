using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Data
{
    public class Library
    {
        [Key]
        public int LibraryId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public int NumOfMembers() {}

        //public int NumOfBooks() { }

        public virtual List<Membership> Memberships { get; set; } = new List<Membership>();
    }
}
