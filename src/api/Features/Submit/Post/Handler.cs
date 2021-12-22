using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geekiam.Data;
using Geekiam.Domain.Requests.Posts;
using Geekiam.Domain.Responses.Posts;
using MediatR;
using Threenine.ApiResponse;

namespace Geekiam.Posts.Service.Features.Submit.Post;

public class Handler : IRequestHandler<Command, SingleResponse<Response>>
{
    private readonly IDataService<Submission, Submitted> _dataService;
    private readonly IMapper _mapper;


    public Handler(IDataService<Submission, Submitted> dataService, IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }
    public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var submission = _mapper.Map<Submission>(request);

        var submitted = await _dataService.Process(submission);

        return new SingleResponse<Response>(new Response { Title = submitted.Article.Title, Url = submitted.Article.Url});
    }
}