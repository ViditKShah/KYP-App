using System.Linq;
using AutoMapper;
using KYP.API.DTOs;
using KYP.API.Models;

namespace KYP.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailedDTO>();
            CreateMap<UserForUpdateDTO, User>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<PhotoForCreationDTO, Photo>();
            CreateMap<UserForRegisterDTO, User>();
            CreateMap<MessageForCreationDTO, Message>();
            CreateMap<Message, MessageForCreationDTO>();
            CreateMap<Message, MessageToReturnDTO>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => {
                    opt.MapFrom(src => src.Sender.Photos
                       .FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.RecipientPhotoUrl, opt => {
                    opt.MapFrom(src => src.Recipient.Photos
                       .FirstOrDefault(p => p.IsMain).Url);
                });
        }
    }
}