using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Mappers
{
    public class AutoMappers :Profile
    {
        public AutoMappers()
        {
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
               src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}