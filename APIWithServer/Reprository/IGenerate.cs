namespace day1.Reprository
{
    public interface IGenerate<T,DTO>
    {
        public List<DTO> GetAll();
        public DTO GetByID(int id);
        public T Insert( T entity);
        public void Delete(int id);
        public void Save();
    }
}
