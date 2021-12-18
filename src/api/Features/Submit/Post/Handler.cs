using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Threenine.ApiResponse;

namespace GeekIAm.Features.Submit.Post
{
    public class Handler : IRequestHandler<Command, SingleResponse<Response>>
    {
      

        public Handler()
        {
          
        }
        public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
         
        

            return new SingleResponse<Response>(new Response { Id = "test string"});
        }
    }
}