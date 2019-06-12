using System;
using System.Collections.Generic;

namespace BankaRenato.WebAPI.Models
{
    public partial class Account
    {
        public Account()
        {
            Card = new HashSet<Card>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Card> Card { get; set; }
    }
}
