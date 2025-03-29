namespace BlazorApp3.Service
{
    public interface IService<T>
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
        public Task insert(T entity);
        public Task Delete(int id);
        public Task Update(int id ,T entity);
    }
}
