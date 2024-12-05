using Beadandó.Shared;

namespace Beadandó.UI.Services
{
    public interface IReaderService
    {
        public Task<List<Reader>> GetAllAsync();

        public Task AddAsync(Reader Reader);

        public Task<Reader> GetAsync(Guid id);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(Reader reader);
    }
}