﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberLend.Models.BookModels
{
    public class BookListItem
    {

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsAvailable { get; set; }
    }
}