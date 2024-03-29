﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.BookModels
{
    public class BookEdit
    {
        public int BookId { get; set; }

        [MinLength(10, ErrorMessage = "Please enter a 10- or 13-digit ISBN")]
        [MaxLength(13, ErrorMessage = "Please enter a 10- or 13-digit ISBN")]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author's First Name")]
        public string AuthorFirstName { get; set; }

        [Required]
        [Display(Name = "Author's Last Name")]
        public string AuthorLastName { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        public string Edition { get; set; }

        public string Genre { get; set; }
    }
}
