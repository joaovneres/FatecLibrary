using FatecLibrary.BookAPI.Context;
using FatecLibrary.BookAPI.Models.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;

namespace FatecLibrary.BookAPI.Repositories.Entities;

public class PublishingRepository : IPublishingRepository
{

    // o que fazem os repositories?
    // Fazem o acesso dos métodos ao banco de dados

    private readonly AppDBContext _dBContext;

    public PublishingRepository(AppDBContext dBContext)
    {
        _dbContext  = dBContext;
    }

    public Task<Publishing> Create(Publishing publishing)
    {
        throw new NotImplementedException();
    }

    public Task<Publishing> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Publishing>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Publishing> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Publishing>> GetPublishersBooks()
    {
        throw new NotImplementedException();
    }

    public Task<Publishing> Update(Publishing publishing)
    {
        throw new NotImplementedException();
    }
}
