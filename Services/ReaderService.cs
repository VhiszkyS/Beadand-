using Beadandó.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Beadandó
{
    public class ReaderService : IReaderService
    {
        private ReadersContext _context;
        private ILogger<ReaderService> _logger;

        public ReaderService(ILogger<ReaderService> logger, ReadersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddAsync(Reader reader)
        {
            _logger.LogInformation("Reader to add: {@Reader}", reader);

            await _context.AddAsync(reader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reader = await GetAsync(id);

            if (reader is null)
            {
                throw new KeyNotFoundException("Reader not found");
            }

            _context.RemoveRange(reader);
            _context.Remove(reader);
            await _context.SaveChangesAsync();
        }

        public async Task<Reader> GetAsync(Guid id)
        {
            return await _context.FindAsync<Reader>(id);
        }

        public async Task<List<Reader>> GetAllAsync()
        {
            _logger.LogInformation("All readers retrieved");
            return await _context.Readers.ToListAsync();
        }

        public async Task UpdateAsync(Reader newReader)
        {
            var existingBook = await GetAsync(newReader.Id);

            existingBook.Name = newReader.Name;
            existingBook.Address = newReader.Address;
            existingBook.BirthDate = newReader.BirthDate;
            
            await _context.SaveChangesAsync();
        }
    }
}
