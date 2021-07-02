using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.LibraryModels
{
    public class LibraryDetails
    {
        public int LibraryId { get; set; }

        public string ApplicationUserId { get; set; }

        //If the current user is a member of the library, the appropriate MembershipId will be assigned.
        //If the current user is not a member, a value of 0 will be assigned.
        public int MembershipId { get; set; }

        [Display(Name = "Library Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Library Caretaker")]
        public string CaretakerName { get; set; }

        [Display(Name = "Number of Members")]
        public int NumOfMembers { get; set; }

        [Display(Name = "Number of Books")]
        public int NumOfBooks { get; set; }
    }
}
