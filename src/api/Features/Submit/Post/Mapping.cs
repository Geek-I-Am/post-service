using System;
using AutoMapper;
using Geekiam.Domain.Requests.Posts;
using GeekIAm.Features.Submit.Post.Models;

namespace GeekIAm.Features.Submit.Post
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<string, Uri>().ConvertUsing<StringToUriConverter>();
            CreateMap<Command, Submission>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Article.Categories))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Article.Tags));

            CreateMap<PostBody, Detail>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Published));
        }
    }

    public class StringToUriConverter : ITypeConverter<string, Uri>
    {
        public Uri Convert(string source, Uri destination, ResolutionContext context)
        {
            Uri.TryCreate(source, UriKind.Absolute, out destination);
            return destination;
        }
    }
}