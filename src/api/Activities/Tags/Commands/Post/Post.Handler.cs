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


namespace Boleyn.Service.Activities.Tags.Commands.Post;

public class Handler : IRequestHandler<Command, SingleResponse<Response>>
{
    private readonly IUnitOfWork _unitOfWork;

    public Handler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var newTag = new
            Geekiam.Database.Entities.Tags { Name = request.Tag.ToTag(), Permalink = request.Tag.ToPermalink() };
        
        var articleTag = _unitOfWork.GetRepository<Geekiam.Database.Entities.Tags>().InsertNotExists(x => x.Name == request.Tag.ToTag(), newTag);
       await _unitOfWork.CommitAsync();
        
      var poo =  _unitOfWork.GetRepository<ArticleTags>().Insert(new ArticleTags { ArticleId = request.Id, TagId = articleTag.Id });
      await _unitOfWork.CommitAsync();
        return new SingleResponse<Response>(new Response{ Id = poo.Id});
    }
}
