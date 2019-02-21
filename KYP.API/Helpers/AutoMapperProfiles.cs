using AutoMapper;
using KYP.API.DTOs;
using KYP.API.Models;

namespace KYP.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>();
            CreateMap<User, UserForDetailedDTO>();
        }
    }
}