using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Dtos
{
    public class CardForDashboardDto
    {
        public int Id { get; set; }
        public int CardType { get; set; }
        public string Type { get; set; }
    }
}
