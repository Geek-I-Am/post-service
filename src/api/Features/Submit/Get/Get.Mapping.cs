using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Geekiam.Database.Entities;

namespace Geekiam.Posts.Service.Features.Submit.Get;

public class Mapping : Profile
{
    public Mapping()
    {
        
        CreateMap<Articles, Response>(MemberList.None)
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ArticleTags.Select(x => x.Tag).ToArray()));



    }

}
