using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geek.Database.Entities;
using GeekIAm.Data.Services;
using GeekIAm.Domain.Requests.Articles;
using GeekIAm.Domain.Responses.Articles;
using MediatR;
using Threenine.ApiResponse;
using Threenine.Data;


namespace GeekIAm.Features.Submit.Post
{
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
            var submission = _mapper.Map<Submission>(request.Article);
            var result = await _dataService.Process(submission);
        

            return new SingleResponse<Response>(new Response { Id = result.ToString()});
        }
    }
}