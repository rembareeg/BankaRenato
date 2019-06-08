using AutoMapper;
using BankaRenato.WebAPI.Data;
using BankaRenato.WebAPI.Dtos;
using BankaRenato.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _repo;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _repo.GetUser(id);

            UserForDashboardDto dashboardUser = _mapper.Map<UserForDashboardDto>(user);

            return Ok(dashboardUser);
        }

        [HttpPost("openaccount")]
        public async Task<IActionResult> OpenAccount(int id)
        {
            User user = await _repo.GetUser(id);

            UserForDashboardDto dashboardUser = _mapper.Map<UserForDashboardDto>(user);

            return Ok(dashboardUser);
        }


    }
}
