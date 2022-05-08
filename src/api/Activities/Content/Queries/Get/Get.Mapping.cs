using System.Linq;
using AutoMapper;
using Geekiam.Database.Entities;

namespace Boleyn.Service.Activities.Posts.Queries.Get;

public class Mapping : Profile
{
    public Mapping()
    {
          CreateMap<Articles, Response>(MemberList.None)
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
    }
}
