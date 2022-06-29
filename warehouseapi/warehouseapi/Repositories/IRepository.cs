namespace warehouseapi.Repositories
{
    public interface IRepository<T>
    {
        public T Create(T _object);
        public T? Update(T _object);
        public IEnumerable<T> GetAll();
        public T? GetById(Guid id);
        public void Delete(Guid id);
    }
}
