using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Dtos
{
    public class UserOwnsAccount
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
    }
}
