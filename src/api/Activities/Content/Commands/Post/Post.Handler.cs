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

namespace Boleyn.Service.Activities.Posts.Commands.Post
{
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
            _unitOfWork.GetRepository<Articles>().Insert(article);
            await _unitOfWork.CommitAsync();
            return new SingleResponse<Response>(new Response { Id = article.Id});
        }
    }
}