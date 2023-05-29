using FatecLibrary.Web.Models.Entities;

namespace FatecLibrary.Web.Services.Interfaces;

public interface IPublishingService
{
    Task<IEnumerable<PublishingViewModel>> GetAllPublishers(string token);
    Task<PublishingViewModel> FindPublishingById(int id, string token);
    Task<PublishingViewModel> CreatePublishing(PublishingViewModel publishingVM, string token);
    Task<PublishingViewModel> UpdatePublishing(PublishingViewModel publishingVM, string token);
    Task<bool> DeletePublishing(int id, string tokenk);
}
