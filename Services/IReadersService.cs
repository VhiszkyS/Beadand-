using BeadandóShared;

namespace BeadandóUI.Services
{
    public interface IReadersService
    {
        public Task<List<Reader>> GetAllAsync();

        public Task AddAsync(Reader Reader);

        public Task<Reader> GetAsync(Guid id);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(Reader reader);
    }
}
