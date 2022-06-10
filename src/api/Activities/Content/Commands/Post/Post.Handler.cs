using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geekiam.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Threenine;
using Threenine.ApiResponse;
using Threenine.Data;

namespace Boleyn.Service.Activities.Posts.Commands.Post
{
    public class Handler : IRequestHandler<Command, SingleResponse<Response>>
    {
        private readonly IDataService _service;

        public Handler(IDataService service)
        {
            _service = service;
        }

        public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _service.Create<Articles, Article, Response>(request.Article);
           
        }
    }
}