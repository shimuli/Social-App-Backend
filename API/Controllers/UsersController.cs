using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Repository.IRepo;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Authorize]

    public class UsersController : BaseApiController
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepo userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepo.GetUserAsync();
            var usersDto = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUsers(string username)
        {
            var user = await _userRepo.GetUserDtoAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            //var userDto = _mapper.Map<MemberDto>(user);
            return Ok(user);
        }

        [HttpGet("{id}")]
      
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepo.GetUsByIDAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<MemberDto>(user);
            return Ok(userDto);
        }
    }
}