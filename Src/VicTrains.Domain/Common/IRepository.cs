namespace VicTrains.Domain.Common
{
    // persistent store for domain aggregate roots
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
