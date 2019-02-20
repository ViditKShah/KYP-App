using System;

namespace KYP.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAddded { get; set; }
        public bool IsMain { get; set; }
    }
}