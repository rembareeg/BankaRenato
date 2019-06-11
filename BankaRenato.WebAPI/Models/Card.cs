using System;
using System.Collections.Generic;

namespace BankaRenato.WebAPI.Models
{
    public partial class Card
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CardType { get; set; }

        public virtual Account Account { get; set; }
        public virtual CardType CardTypeNavigation { get; set; }
    }
}
