using System;
using System.Collections.Generic;

namespace BankaRenato.WebAPI.Models
{
    public partial class CardType
    {
        public CardType()
        {
            Card = new HashSet<Card>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Card> Card { get; set; }
    }
}
