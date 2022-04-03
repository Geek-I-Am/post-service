using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geekiam.Database.Entities;
using MediatR;
using Threenine.ApiResponse;
using Threenine.Data;

namespace Geekiam.Posts.Service.Features.Submit.Post;

public class Handler : IRequestHandler<Command, SingleResponse<Response>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public Handler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var article = _mapper.Map<Articles>(request.Article);
        var articlesRepository = _unitOfWork.GetRepository<Articles>();
        articlesRepository.Insert(article);
        await _unitOfWork.CommitAsync();
        
        if (request.Article.Metadata.Tags != null) SaveTags(request.Article.Metadata.Tags.ToList(), article.Id);
        if (request.Article.Metadata.Categories != null) SaveCategories(request.Article.Metadata.Categories.ToList(), article.Id);

        return new SingleResponse<Response>(new Response { Title = article.Title, Url = new Uri(article.Url)});
    }
    
    internal void SaveCategories(List<string> categories, Guid articleId)
    {
        var categoriesRepository = _unitOfWork.GetRepository<Categories>();
       
        categories.ForEach(category =>
        {
            var categoryName = TransformCategory(category);
            var categoryLink = TransformPermalink(category);

            var newCat = new
                Categories { Name = categoryName, Permalink = categoryLink, Created = DateTime.Now };

            var articleCategory = categoriesRepository.InsertNotExists(x => x.Name == categoryName, newCat);
            _unitOfWork.Commit();

            var articleCategoriesRepository = _unitOfWork.GetRepository<ArticleCategories>();
            articleCategoriesRepository.Insert(new ArticleCategories()
                { ArticleId = articleId, CategoryId = articleCategory.Id });
            
            _unitOfWork.Commit();
        });
    }

    internal void SaveTags(List<string> tags, Guid articleId)
    {
        var tagsRepository = _unitOfWork.GetRepository<Tags>();
     
        tags.ForEach(tag =>
        {
            var tagName = TransformTag(tag);
            var tagLink = TransformPermalink(tag);

            var newTag = new
                Tags { Name = tagName, Permalink = tagLink, Created = DateTime.Now };

            var articleTag = tagsRepository.InsertNotExists(x => x.Name == tagName, newTag);
            _unitOfWork.Commit();
            var articleCategoriesRepository = _unitOfWork.GetRepository<ArticleTags>();
            articleCategoriesRepository.Insert(new ArticleTags()
                { ArticleId = articleId, TagId = articleTag.Id });
         
            _unitOfWork.Commit();
        });
    }
    internal string TransformTag(string tag)
    {
        var sb = new StringBuilder();

        var words = tag.Trim().Split(' ');

        if (words.Length < 1)
            return sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tag.ToLower().Trim())).ToString();
        foreach (var word in words)
        {
            sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.Trim().ToLower()));
        }

        return sb.ToString();
    }

    internal static string TransformPermalink(string tag)
    {
        var sb = new StringBuilder();

        var words = tag.Trim().Split(' ');

        if (words.Length < 1) return sb.Append(tag.ToLower().Trim()).ToString();
        
        words.ToList().ForEach(word =>
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                sb.Append($"{word}-");
            }
        });
        

        return sb.ToString().TrimEnd('-');
    }

    internal static string TransformCategory(string text)
    {
        var sb = new StringBuilder();

        var words = text.Trim().Split(' ');

        if (words.Length < 1) return sb.Append(text.ToLower().Trim()).ToString();
        
        words.ToList().ForEach(word =>
        {
            if (!string.IsNullOrWhiteSpace(word))
                sb.Append($" {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.Trim().ToLower())}");
        });

        return sb.ToString().Trim();
    }
}