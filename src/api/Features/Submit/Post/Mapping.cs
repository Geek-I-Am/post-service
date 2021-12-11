using AutoMapper;
using Geek.Database.Entities;
using PostService.Features.Submit.Post.Models;


namespace PostService.Features.Submit.Post
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap<PostBody, Articles>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Published))
                ;
        }
    }
}