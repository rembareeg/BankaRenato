using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Dtos
{
    public class AccountForDashboardDto
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public ICollection<CardForDashboardDto> Cards { get; set; }
    }
}
