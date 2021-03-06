﻿using AutoMapper;
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
        private readonly IAuthRepository _auth;

        public DashboardController(IDashboardRepository repo, IMapper mapper, IAuthRepository auth)
        {
            _repo = repo;
            _mapper = mapper;
            _auth = auth;
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
            IEnumerable<CardForDashboardDto> cards = await _repo.GetAccountCards(account.Id);

            AccountForDashboardDto accountToReturn = _mapper.Map<AccountForDashboardDto>(account);
            accountToReturn.Cards = cards.ToList();

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
        public async Task<IActionResult> OpenAccount(AccountForOpenDto user)
        {
            if (await _repo.OpenAccount(user.UserId, user.Name))
            {
                return Ok();
            }

            return Unauthorized();
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpPost("createcard")]
        public async Task<IActionResult> CreateCard(CardForCreateDto card)
        {
            if (await _repo.CreateCard(card.AccountId, card.CardType))
            {
                return Ok();
            }

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
            if (await _repo.DeleteUser(id))
            {
                return Ok();
            }

            return Unauthorized();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteaccount/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (await _repo.DeleteAccount(id))
            {
                return Ok();
            }

            return Unauthorized();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("deletecard/{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            if (await _repo.DeleteCard(id))
            {
                return Ok();
            }

            return Unauthorized();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("updatecard")]
        public async Task<IActionResult> UpdateCard(CardForDashboardDto card)
        {
            if (await _repo.UpdateCard(card.Id, card.CardType))
            {
                return Ok();
            }

            return Unauthorized();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("updateuserasadmin")]
        public async Task<IActionResult> UpdateUserAsAdmin(UserForUpdateAsAdminDto userForUpdate)
        {

            User user = await _repo.GetUser(userForUpdate.Id);
            //check if password is not null and between <5, 15>
            if (!String.IsNullOrEmpty(userForUpdate.Password) && (userForUpdate.Password.Length < 5 || userForUpdate.Password.Length > 15))
            {
                return BadRequest("Password needs to be between 5 and 15 characters");
            }
            //if email is not same check if newe mail already exists
            if (user.Email != userForUpdate.Email.ToLower())
            {
                if (await _auth.EmailExists(userForUpdate.Email.ToLower()))
                {
                    return BadRequest("Email already in use");
                }

            }
            //if username is not same check if new username already exists
            if (user.Username != userForUpdate.Username.ToLower())
            {
                if (await _auth.UserExists(userForUpdate.Username.ToLower()))
                {
                    return BadRequest("Username already in use");
                }

            }

            if (await _repo.UpdateUserAsAdmin(userForUpdate))
            {
                userForUpdate.Password = "";
                return Ok(userForUpdate);
            }

            return Unauthorized();
        
        }
        [Authorize(Roles = "Client")]
        [HttpPut("updateuserasuser")]
        public async Task<IActionResult> UpdateUserAsUser(UserForUpdateAsUserDto userForUpdate)
        {
            User user = await _repo.GetUser(userForUpdate.Id);
            //check if password is not null and between <5, 15>
            if (!String.IsNullOrEmpty(userForUpdate.Password) && (userForUpdate.Password.Length < 5 || userForUpdate.Password.Length > 15))
            {
                return BadRequest("Password needs to be between 5 and 15 characters");
            }
            //if email is not same check if new email already exists
            if (user.Email != userForUpdate.Email.ToLower())
            {
                if (await _auth.EmailExists(userForUpdate.Email.ToLower()))
                {
                    return BadRequest("Email already in use");
                }

            }

            if (await _repo.UpdateUserAsUser(userForUpdate))
            {
                userForUpdate.Password = "";
                return Ok(userForUpdate);
            }

            return Unauthorized();
            
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("updateaccount")]
        public async Task<IActionResult> UpdateAccount(AccountForUpdateDto account)
        {
            if (await _repo.UpdateAccount(account))
            {
                return Ok();
            }

            return Unauthorized();
        }
        [Authorize(Roles = "Client")]
        [HttpPost("userownsaccount")]
        public async Task<IActionResult> UserOwnsAccount(UserOwnsAccount model)
        {
            if (await _repo.UserOwnsAccount(model.UserId, model.AccountId))
            {
                return Ok();
            }

            return Unauthorized();
        }



    }
}
