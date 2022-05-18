using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geekiam.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
          return  await CreatePost(_mapper.Map<Articles>(request.Article));
        }

        private async Task<SingleResponse<Response>> CreatePost(Articles article)
        {
            try
            {
              var created =  _unitOfWork.GetRepository<Articles>().Insert(article);
                await _unitOfWork.CommitAsync();
                return new SingleResponse<Response>(_mapper.Map<Response>(created));
            }
            catch (DbUpdateException dex)
            {
                return new SingleResponse<Response>(null, new List<KeyValuePair<string, string[]>>()
                {
                    new("Conflict", new []{"Article with URL Already exists", dex.InnerException.Message})
                });
            }
        }
    }
}