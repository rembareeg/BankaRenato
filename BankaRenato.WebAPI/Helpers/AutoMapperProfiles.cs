using AutoMapper;
using BankaRenato.WebAPI.Dtos;
using BankaRenato.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDashboardDto>();
            CreateMap<Account, AccountForDashboardDto>().ForMember(member => member.Currency, opt => {
                opt.MapFrom(source => Valuta(source.Currency));
            });
        }

        private string Valuta(int valuta)
        {
            
            switch (valuta)
            {
                case 191:
                    return "HRK";
                case 978:
                    return "EUR";
                case 826:
                    return "GBP";
                case 840:
                    return "USD";
                default:
                    return "???";
            }
        }
    }
}
