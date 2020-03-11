using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookExchanger.Models
{
    public class RequestBookViewModel
    {
        [Required]
        public string Tittle { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int point { get; set; }
    }
}