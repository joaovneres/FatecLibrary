using FatecLibrary.Web.Models.Entities;
using FatecLibrary.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FatecLibrary.Web.Services.Entities;

public class PublishingService : IPublishingService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndPoint = "/api/publishing/";
    private PublishingViewModel _publishingViewModel;
    private IEnumerable<PublishingViewModel> publishers;

    public PublishingService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<PublishingViewModel>> GetAllPublishers(string token)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        PutTokenInHeaderAuthorization(token, client);
        var response = await client.GetAsync(apiEndPoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            publishers = await JsonSerializer.DeserializeAsync<IEnumerable
                <PublishingViewModel>>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return publishers;
    }

    public async Task<PublishingViewModel> FindPublishingById(int id, string token)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        PutTokenInHeaderAuthorization(token, client);
        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _publishingViewModel = await JsonSerializer.DeserializeAsync<PublishingViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _publishingViewModel;
    }

    public async Task<PublishingViewModel> CreatePublishing(PublishingViewModel publishingVM, string token)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        PutTokenInHeaderAuthorization(token, client);
        StringContent content = new StringContent(JsonSerializer.Serialize(publishingVM),
            Encoding.UTF8,
            "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _publishingViewModel = await JsonSerializer.DeserializeAsync<PublishingViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _publishingViewModel;
    }

    public async Task<PublishingViewModel> UpdatePublishing(PublishingViewModel publishingVM, string token)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        PutTokenInHeaderAuthorization(token, client);
        PublishingViewModel publishingUpdate = new PublishingViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndPoint, publishingVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                publishingUpdate = await JsonSerializer.DeserializeAsync<PublishingViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return publishingUpdate;
    }
    public async Task<bool> DeletePublishing(int id, string token)
    {
        var client = _clientFactory.CreateClient("BookAPI");
        PutTokenInHeaderAuthorization(token, client);
        using (var response = await client.DeleteAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode) return true;
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Baerer", token);
    }
}
