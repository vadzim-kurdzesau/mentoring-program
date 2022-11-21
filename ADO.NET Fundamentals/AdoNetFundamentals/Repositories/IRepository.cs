namespace AdoNetFundamentals.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds <typeparamref name="T"/> to database.
        /// </summary>
        void Add(T obj);

        /// <summary>
        /// Gets the <typeparamref name="T"/> with specified <paramref name="id"/> from database.
        /// </summary>
        /// <returns>Null, if <typeparamref name="T"/> with specified <paramref name="id"/> does not exist.</returns>
        T? Get(int id);

        /// <summary>
        /// Updates the <typeparamref name="T"/> with the same ID in database.
        /// </summary>
        void Update(T obj);

        /// <summary>
        /// Deletes <typeparamref name="T"/> with specified <paramref name="id"/> from database.
        /// </summary>
        void Delete(int id);
    }
}
