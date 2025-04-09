namespace ApiMendis
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<bool> InsertAsync(TEntity entity);
    }
}
