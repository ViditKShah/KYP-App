using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KYP.API.Data;
using KYP.API.DTOs;
using KYP.API.Helpers;
using KYP.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYP.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IKYPRepository _repo;
        private readonly IMapper _mapper;
        public MessagesController(IKYPRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{messageId}", Name="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int messageId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repo.GetMessage(messageId);

            if (messageFromRepo == null) 
            {
                return NotFound();
            }
            return Ok(messageFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, 
            MessageForCreationDTO messageForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            messageForCreationDto.SenderId = userId;
            
            var recipient = await _repo.GetUser(messageForCreationDto.RecipientId);

            if (recipient == null)
                return BadRequest("Could not find the recipient!");

            var message = _mapper.Map<Message>(messageForCreationDto);

            _repo.Add(message);

            var messageToReturn = _mapper.Map<MessageForCreationDTO>(message);

            if (await _repo.SaveAll())
                return CreatedAtRoute("GetMessage", 
                    new {messageId = message.Id}, messageToReturn);
            
            throw new Exception("Creating the message failed on save!");
        }

    }
}