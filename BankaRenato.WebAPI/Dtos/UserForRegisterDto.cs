using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 2, ErrorMessage = "First name needs to be between 2 and 40 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 2, ErrorMessage = "Last name needs to be between 2 and 40 characters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 5, ErrorMessage = "Password needs to be between 5 and 15 characters")]
        public string Password { get; set; }
    }
}
