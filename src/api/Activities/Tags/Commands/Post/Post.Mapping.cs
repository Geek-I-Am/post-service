using AutoMapper;
using Boleyn.Service.Activities.Tags.Commands;
using Geekiam.Database.Entities;


namespace Boleyn.Service.Activities.Tags.Commands.Post;

public class Mapping: Profile
{
    public Mapping()
    {
        CreateMap<Command, Geekiam.Database.Entities.Tags>(MemberList.None)
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Tag.ToTag()))
            .ForMember(dest => dest.Permalink, opt => opt.MapFrom(src => src.Tag.ToPermalink()))
           ;

    }
}
