using System.Threading.Tasks;
using KYP.API.Data;
using KYP.API.DTOs;
using KYP.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace KYP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.UserName = userForRegisterDTO.UserName.ToLower();

            if (await _repo.UserExists(userForRegisterDTO.UserName))
                return BadRequest("UserName already exists!");

            var userToCreate = new User() 
            {
                UserName = userForRegisterDTO.UserName
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
        }
    }
}