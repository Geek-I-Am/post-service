namespace GeekIAm.Data.Services;

public interface IDataService<in TAggregate, TResult> where TAggregate : class
where TResult : class
{
    Task<TResult> Process(TAggregate aggregate);
}