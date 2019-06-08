using System;
using System.Collections.Generic;

namespace BankaRenato.WebAPI.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
