using System;
using System.Collections.Generic;
using AutoMapper;
using FizzWare.NBuilder;
using Geekiam.Domain.Requests.Posts;
using Geekiam.Posts.Service.Features.Submit.Post;
using Geekiam.Posts.Service.Features.Submit.Post.Models;
using Shouldly;
using Xunit;

namespace Api.Tests.Features.Submit
{
    public class MappingTests
    {
        private IMapper _mapper;
        
        public MappingTests()
        {
            var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<Mapping>());
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void ShouldMapCommandToSubmission()
        {
            var result = _mapper.Map<Submission>(TestCommand);
        
            result.Article.Author.ShouldBeEquivalentTo(TestCommand.Article.Author);
            result.Article.Published.ShouldBeEquivalentTo(TestCommand.Article.Published);
            result.Article.Summary.ShouldBeEquivalentTo(TestCommand.Article.Summary);
            result.Article.Title.ShouldBeEquivalentTo(TestCommand.Article.Title);
            result.Article.Url.ShouldBeOfType<Uri>();
        }

        private Command TestCommand => Builder<Command>.CreateNew()
            .With(x => x.Article = Builder<PostBody>
                .CreateNew()
                .With(x=> x.Url = $"https://{Guid.NewGuid().ToString()}.com")
                .With(x => x.Categories = new List<string>(){"Category1", "Category2"})
                .With(x => x.Tags = new List<string>(){"Tag1", "Tag2"})
                .Build())
            .Build();
    }
}