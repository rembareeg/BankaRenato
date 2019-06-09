using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Dtos
{
    public class CardForCreateDto
    {
        public int AccountId { get; set; }
        public int CardType { get; set; }
    }
}
