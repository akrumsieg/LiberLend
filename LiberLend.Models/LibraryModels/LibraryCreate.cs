using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.LibraryModels
{
    public class LibraryCreate
    {
        [Required]
        [Display(Name = "Library Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
