using System;
using System.Collections.Generic;
using System.Linq;
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

        if (request.Article.Tags != null) SaveTags(request.Article.Tags.ToList(), article.Id);
        if (request.Article.Categories != null)
            SaveCategories(request.Article.Categories.ToList(), article.Id);

        return new SingleResponse<Response>(new Response { Title = article.Title, Url = new Uri(article.Url) });
    }

    private void SaveCategories(List<string> categories, Guid articleId)
    {
        var categoriesRepository = _unitOfWork.GetRepository<Categories>();

        categories.ForEach(category =>
        {
            var newCat = new
                Categories { Name = category.ToCategory(), Permalink = category.ToPermalink(), Created = DateTime.Now };

            var articleCategory = categoriesRepository.InsertNotExists(x => x.Name == category.ToCategory(), newCat);
            _unitOfWork.Commit();

            var articleCategoriesRepository = _unitOfWork.GetRepository<ArticleCategories>();
            articleCategoriesRepository.Insert(new ArticleCategories()
                { ArticleId = articleId, CategoryId = articleCategory.Id });

            _unitOfWork.Commit();
        });
    }

    private void SaveTags(List<string> tags, Guid articleId)
    {
        var tagsRepository = _unitOfWork.GetRepository<Tags>();

        tags.ForEach(tag =>
        {
            var newTag = new
                Tags { Name = tag.ToTag(), Permalink = tag.ToPermalink(), Created = DateTime.Now };

            var articleTag = tagsRepository.InsertNotExists(x => x.Name == tag.ToTag(), newTag);
            _unitOfWork.Commit();
            var articleCategoriesRepository = _unitOfWork.GetRepository<ArticleTags>();
            articleCategoriesRepository.Insert(new ArticleTags()
                { ArticleId = articleId, TagId = articleTag.Id });

            _unitOfWork.Commit();
        });
    }
}