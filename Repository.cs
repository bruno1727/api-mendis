namespace ApiMendis
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public Task InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
