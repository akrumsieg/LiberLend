using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.BookModels
{
    public class BookDetails
    {
        public int BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        public string Edition { get; set; }

        public string Genre { get; set; }
    }
}
