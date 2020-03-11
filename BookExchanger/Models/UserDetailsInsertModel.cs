using System.ComponentModel.DataAnnotations;

namespace BookExchanger.Repository
{
    public class UserDetailsInsertModel
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string Address { get; set; }
    }
}