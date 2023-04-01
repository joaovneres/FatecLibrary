﻿using FatecLibrary.Web.Models.Entities;
using FatecLibrary.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace FatecLibrary.Web.Services.Entities;

public class BookService : IBookService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndPoint = "/api/book/";
    private BookViewModel _bookViewModel;
    private IEnumerable<BookViewModel> books;

    public BookService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<BookViewModel>> GetAllBooks()
    {
        var client = _clientFactory.CreateClient("BookAPI");
        var response = await client.GetAsync(apiEndPoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            books = await JsonSerializer.DeserializeAsync<IEnumerable<BookViewModel>>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return books;
    }

    public async Task<BookViewModel> FindBookById(int id)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _bookViewModel = await JsonSerializer.DeserializeAsync<BookViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _bookViewModel;
    }

    public async Task<BookViewModel> CreateBook(BookViewModel bookVM)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        StringContent content = new StringContent(JsonSerializer.Serialize(bookVM),
            Encoding.UTF8,
            "aplication/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _bookViewModel = await JsonSerializer.DeserializeAsync<BookViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _bookViewModel;
    }

    public async Task<BookViewModel> UpdateBook(BookViewModel bookVM)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        BookViewModel bookUpdate = new BookViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndPoint, bookVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                bookUpdate = await JsonSerializer.DeserializeAsync<BookViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return bookUpdate;
    }

    public async Task<bool> DeleteBookById(int id)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        using (var response = await client.DeleteAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode) return true;
        }
        return false;
    }
}
