using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookExchanger.Models
{
    public class BookDetails
    {
        [Required]
        public String Title { get; set;}

        [Required]
        public String Edition { get; set; }

        [Required]
        public int UsedDate { get; set; }

        
        [Required]
        public String Author { get; set; }

        [Required]
        public int point { get; set; }


    }
}