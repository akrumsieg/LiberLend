using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.LibraryModels
{
    public class LibraryEdit
    {
        public int LibraryId { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "Library Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
