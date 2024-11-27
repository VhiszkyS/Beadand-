using BeadandóShared;
using System.Net.Http.Json;
namespace BeadandóUI.Services
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Loan>>("loans");
        }

        public async Task AddAsync(Loan loan)
        {
            await _httpClient.PostAsJsonAsync("loans", loan);
        }

        public async Task<Loan> GetAsync(Guid BookId)
        {
            return await _httpClient.GetFromJsonAsync<Loan>($"loans/{BookId}");
        }

        public async Task DeleteAsync(Guid BookId)
        {
            await _httpClient.DeleteAsync($"loans/{BookId}");
        }

        public async Task UpdateAsync(Loan loan)
        {
            await _httpClient.PutAsJsonAsync<Loan>($"loans/{loan.BookId}", loan);
        }

    }
}
