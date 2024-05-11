namespace ApiMendis
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task InsertAsync(TEntity entity);
    }
}
