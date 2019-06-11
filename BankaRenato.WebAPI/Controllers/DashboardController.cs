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

        [Authorize(Roles = "Client, Admin")]
        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _repo.GetUser(id);
            IEnumerable<Account> accounts = await _repo.GetUserAccounts(id);

            UserForDashboardDto dashboardUser = _mapper.Map<UserForDashboardDto>(user);
            dashboardUser.Accounts = _mapper.Map<IEnumerable<AccountForDashboardDto>>(accounts).ToList();           

            return Ok(dashboardUser);
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpGet("getaccount/{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            Account account = await _repo.GetUserAccount(id);
            IEnumerable<Card> cards = await _repo.GetAccountCards(account.Id);

            AccountForDashboardDto accountToReturn = _mapper.Map<AccountForDashboardDto>(account);
            accountToReturn.Cards = _mapper.Map<IEnumerable<CardForDashboardDto>>(cards).ToList();

            return Ok(accountToReturn);
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpGet("getcardtypes")]
        public async Task<IActionResult> GetCardTypes()
        {
            return Ok((await _repo.GetCardTypes()).ToList());
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpPost("openaccount")]
        public async Task<IActionResult> OpenAccount(UserForDashboardDto user)
        {
            if (await _repo.OpenAccount(user.Id)) return Ok();

            return Unauthorized();
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpPost("createcard")]
        public async Task<IActionResult> CreateCard(CardForCreateDto card)
        {
            if (await _repo.CreateCard(card.AccountId, card.CardType)) return Ok();

            return Unauthorized();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getusersbyrole/{id}")]
        public async Task<IActionResult> GetUsersByRole(string id)
        {
            return Ok((await _repo.GetUsersByRole(id)).ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _repo.DeleteUser(id)) return Ok();

            return Unauthorized();
        }



    }
}
