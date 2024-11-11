namespace Beadandó
{
    public interface IReaderService
    {
        Task AddAsync(Reader reader);

        Task DeleteAsync(Guid Id);

        Task <Reader> GetAsync(Guid Id);

        Task <List<Reader>> GetAllAsync();

        Task UpdateAsync(Reader newReader);
    }
}
