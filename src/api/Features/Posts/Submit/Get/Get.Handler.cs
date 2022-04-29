using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geekiam.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Threenine.ApiResponse;
using Threenine.Data;

namespace Geekiam.Posts.Service.Features.Posts.Get;

public class Handler : IRequestHandler<Query, SingleResponse<Response>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Handler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<SingleResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepositoryAsync<Articles>();
        var article = await repo.SingleOrDefaultAsync(x => x.Id == request.Id, include: inc => inc.Include(x => x.ArticleTags));

        return new SingleResponse<Response>(_mapper.Map<Response>(article));

    }
}