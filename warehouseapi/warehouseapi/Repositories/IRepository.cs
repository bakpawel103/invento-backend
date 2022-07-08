namespace warehouseapi.Repositories
{
    public interface IRepository<T>
    {
        public void InitializeDatabaseFile();

        public void Save(List<T> objects);

        public List<T> LoadDatabase();
    }
}
