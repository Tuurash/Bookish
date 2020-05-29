using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Models.DTOs
{
    public class BooksDTO
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public int BookEdition { get; set; }
        public int Point { get; set; }
        public string UploadedBy { get; set; }
    }
}