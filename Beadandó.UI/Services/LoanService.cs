using BeadandóShared;
using System.Net.Http.Json;
namespace Beadandó.UI.Services
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
            return await _httpClient.GetFromJsonAsync<List<Loan>>("https://localhost:5001/loans");
        }

        public async Task AddAsync(Loan loan)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:5001/loans", loan);
        }

        public async Task<Loan> GetAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Loan>($"https://localhost:5001/loans/{id}");
        }

        public async Task DeleteAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"https://localhost:5001/loans/{id}");
        }

        public async Task UpdateAsync(Loan loan)
        {
            await _httpClient.PutAsJsonAsync<Loan>($"https://localhost:5001/loans/{loan.Id}", loan);
        }

    }
}
