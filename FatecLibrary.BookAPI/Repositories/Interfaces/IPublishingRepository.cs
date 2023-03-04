using FatecLibrary.BookAPI.Models.Entities;

namespace FatecLibrary.BookAPI.Repositories.Interfaces;

public interface IPublishingRepository
{
    Task GetPublishersBook { get; }

    Task<IEnumerable<Publishing>> GetAll();
    Task<IEnumerable<Publishing>> GetPublishersBooks();
    Task<Publishing> GetById(int id);
    Task<Publishing> Create(Publishing publishing);
    Task<Publishing> Update(Publishing publishing);
    Task<Publishing> Delete(int id);

}
