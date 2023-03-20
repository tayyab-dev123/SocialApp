using AutoMapper;
using SocialApp.DTOs;
using SocialApp.Entities;
using SocialApp.Extensions;

namespace SocialApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //From     //To
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dest => dest.photoUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
