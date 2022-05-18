using AutoMapper;
using Geekiam.Database.Entities;

namespace Boleyn.Service.Activities.Posts.Commands.Post
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Article, Articles>(MemberList.None)
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

            CreateMap<Articles, Response>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        }
    
    }
}