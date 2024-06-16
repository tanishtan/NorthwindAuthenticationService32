namespace ProductsAPI.Infrastructure
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetBy(string filterCriteria);
        TEntity GetById(int id);
        void CreateNew(TEntity item);
        void Update(TEntity item);
        void Remove(int id);
    }
}
