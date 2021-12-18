using AutoMapper;
using Geek.Database.Entities;
using GeekIAm.Domain.Requests.Articles;
using GeekIAm.Domain.Responses.Articles;
using Threenine.Data;

namespace GeekIAm.Data.Services;

public class ArticleDataService : IDataService<Submission, Submitted>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ArticleDataService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Submitted> Process(Submission aggregate)
    {
  
        var article =  _mapper.Map<Articles>(aggregate);
         var repo = _unitOfWork.GetRepository<Articles>();
         repo.Insert(article);
         await _unitOfWork.CommitAsync();


         return new Submitted();
    }
}