using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Dtos
{
    public class AccountForOpenDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
