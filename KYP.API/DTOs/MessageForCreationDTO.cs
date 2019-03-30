using System;

namespace KYP.API.DTOs
{
    public class MessageForCreationDTO
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime DateSent { get; set; }
        public string Content { get; set; }

        public MessageForCreationDTO()
        {
            DateSent = DateTime.Now;
        }
    }
}