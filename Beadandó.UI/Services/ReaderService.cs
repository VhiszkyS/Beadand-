using Beadandó.Shared;
using System.Net.Http.Json;
namespace Beadandó.UI.Services;

public class ReaderService : IReaderService
{
    private readonly HttpClient _httpClient;

    public ReaderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Reader>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Reader>>("https://localhost:5001/readers");
    }
    
    public async Task AddAsync(Reader reader)
    {
        await _httpClient.PostAsJsonAsync("https://localhost:5001/readers", reader);
    }

    public async Task<Reader> GetAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Reader>($"https://localhost:5001/readers/{id}");
    }

    public async Task DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"https://localhost:5001/readers/{id}");
    }

    public async Task UpdateAsync(Reader reader)
    {
        await _httpClient.PutAsJsonAsync<Reader>($"https://localhost:5001/readers/{reader.Id}", reader);
    }

}
