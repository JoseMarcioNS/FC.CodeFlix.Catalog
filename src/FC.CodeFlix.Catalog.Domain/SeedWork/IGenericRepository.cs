namespace FC.CodeFlix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<IAggregate> : IRepository
    {
        public Task Insert(IAggregate aggregate, CancellationToken cancellationToken);
        public Task<IAggregate> Get(Guid id, CancellationToken cancellationToken);
        public Task Delete(IAggregate aggregate, CancellationToken cancellationToken);
    }
}
