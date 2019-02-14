using System.Threading.Tasks;
using KYP.API.Data;
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
        public async Task<IActionResult> Register(string userName, string password) 
        {
            // validate

            userName = userName.ToLower();

            if (await _repo.UserExists(userName))
                return BadRequest("UserName already exists!");

            var userToCreate = new User() 
            {
                UserName = userName
            };

            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}