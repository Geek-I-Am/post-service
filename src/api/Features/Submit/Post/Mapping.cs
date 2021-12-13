using AutoMapper;
using Geek.Database.Entities;
using GeekIAm.Domain.Requests.Articles;
using GeekIAm.Features.Submit.Post.Models;


namespace GeekIAm.Features.Submit.Post
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap<PostBody, Submission>()
                .ForMember(dest => dest.Article.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Article.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Article.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Article.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Article.Published, opt => opt.MapFrom(src => src.Published));

            CreateMap<Submission, Articles>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Article.Summary))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Article.Url))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Article.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Article.Author))
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Article.Published));
        }
    }
}