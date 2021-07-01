using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.MembershipModels
{
    public class MembershipDetails
    {
        public int MembershipId { get; set; }

        public string LibraryName { get; set; }

        public string ApplicationUserName { get; set; }

        [Display(Name = "Authorized Membership")]
        public bool IsAuthorized { get; set; }
    }
}
