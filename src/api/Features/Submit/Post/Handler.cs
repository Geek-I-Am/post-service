using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geek.Database.Entities;
using MediatR;
using Threenine.ApiResponse;
using Threenine.Data;


namespace GeekIAm.Features.Submit.Post
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
            var repo = _unitOfWork.GetRepository<Articles>();
            repo.Insert(article);
            await _unitOfWork.CommitAsync();

            return new SingleResponse<Response>(new Response { Id = article.Id.ToString()});
        }
    }
}