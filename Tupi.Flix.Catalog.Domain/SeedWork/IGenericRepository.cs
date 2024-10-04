namespace Tupi.Flix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<T> : IRepository
    {
        public Task Insert(T entity, CancellationToken cancellationToken);
    }
}
