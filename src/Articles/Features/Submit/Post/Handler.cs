using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Threenine.ApiResponse;

namespace Articles.Features.Submit.Post
{
    public class Handler : IRequestHandler<Command, SingleResponse<Response>>
    {
        public Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}