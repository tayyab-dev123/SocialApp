using AutoMapper;
using SocialApp.DTOs;
using SocialApp.Entities;

namespace SocialApp.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
                      //From     //To
            CreateMap<AppUser, MemberDTO>();
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
