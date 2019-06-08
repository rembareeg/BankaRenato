using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaRenato.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankaRenato.WebAPI.Data
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly BankaRenatoDBContext _context;

        public DashboardRepository(BankaRenatoDBContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(int id)
        {
           return await _context.User.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
