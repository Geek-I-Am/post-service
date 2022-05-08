using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geekiam.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Threenine.ApiResponse;
using Threenine.Data;

namespace Boleyn.Service.Activities.Posts.Queries.Get;

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
        var article = await _unitOfWork.GetRepositoryAsync<Articles>()
            .SingleOrDefaultAsync(x => x.Id == request.Id);

        return new SingleResponse<Response>(_mapper.Map<Response>(article));

    }
}