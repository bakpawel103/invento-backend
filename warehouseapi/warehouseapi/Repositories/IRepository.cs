namespace warehouseapi.Repositories
{
    public interface IRepository<T>
    {
        public T Create(T _object);
        public T? Update(T _object);
        public List<T> GetAll();
        public T? GetById(Guid id);
        public bool Delete(Guid id);
    }
}
