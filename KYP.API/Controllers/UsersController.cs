using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KYP.API.Data;
using KYP.API.DTOs;
using KYP.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYP.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IKYPRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IKYPRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst
                (ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "male" : "female";
            }

            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, 
                users.TotalItems, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{userId}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _repo.GetUser(userId);

            var userToReturn = _mapper.Map<UserForDetailedDTO>(user);
            
            return Ok(userToReturn);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserForUpdateDTO userForUpdateDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(userId);
            
            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();
            
            throw new Exception($"Updating user {userId} failed on save");
        }
    }
}