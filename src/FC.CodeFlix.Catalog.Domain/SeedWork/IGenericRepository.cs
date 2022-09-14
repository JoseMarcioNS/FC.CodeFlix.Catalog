namespace FC.CodeFlix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<IAggregate> : IRepository
    {
       public Task Insert(IAggregate aggregate, CancellationToken cancellationToken);
    }
}
