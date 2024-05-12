namespace ApiMendis
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public Task<bool> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
