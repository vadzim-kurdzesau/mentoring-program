namespace OrmFundamentals.Shared
{
    public interface IRepository<T>
    {
        void Add(T entry);

        T? Get(int id);

        IEnumerable<T> GetAll();

        void Update(T entry);

        void Delete(int id);
    }
}
