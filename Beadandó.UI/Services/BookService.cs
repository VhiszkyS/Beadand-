using BeadandóShared;
using System.Net.Http.Json;

namespace Beadandó.UI.Services;

public class BookService : IBookService
{
    private readonly HttpClient _httpClient;

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Book>>("https://localhost:5001/book");
    }

    public async Task AddAsync(Book book)
    {
        await _httpClient.PostAsJsonAsync("https://localhost:5001/book", book);
    }

    public async Task<Book> GetAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Book>($"https://localhost:5001/book/{id}");
    }

    public async Task DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"https://localhost:5001/book/{id}");
    }

    public async Task UpdateAsync(Book book)
    {
        await _httpClient.PutAsJsonAsync<Book>($"https://localhost:5001/book/{book.Id}", book);
    }
}
