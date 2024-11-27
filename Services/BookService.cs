﻿using BeadandóShared;
using System.Net.Http.Json;

namespace BeadandóUI.Services;

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
        await _httpClient.PostAsJsonAsync("books", book);
    }

    public async Task<Book> GetAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Book>($"books/{id}");
    }

    public async Task DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"books/{id}");
    }

    public async Task UpdateAsync(Book book)
    {
        await _httpClient.PutAsJsonAsync<Book>($"books/{book.Id}", book);
    }

}