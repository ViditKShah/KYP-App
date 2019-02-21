using System;

namespace KYP.API.DTOs
{
    public class PhotosForDetailedDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAddded { get; set; }
        public bool IsMain { get; set; }
    }
}