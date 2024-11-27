using BeadandóShared;
using System.Net.Http.Json;
namespace BeadandóUI.Services;

public class ReadersService : IReadersService
{
    private readonly HttpClient _httpClient;

    public ReadersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Reader>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Reader>>("readers");
    }

    public async Task AddAsync(Reader reader)
    {
        await _httpClient.PostAsJsonAsync("readers", reader);
    }

    public async Task<Reader> GetAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Reader>($"readers/{id}");
    }

    public async Task DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"readers/{id}");
    }

    public async Task UpdateAsync(Reader reader)
    {
        await _httpClient.PutAsJsonAsync<Reader>($"readers/{reader.Id}", reader);
    }

}
