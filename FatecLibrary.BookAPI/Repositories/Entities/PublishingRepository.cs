using FatecLibrary.BookAPI.Context;
using FatecLibrary.BookAPI.Models.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecLibrary.BookAPI.Repositories.Entities;

public class PublishingRepository : IPublishingRepository
{

    // o que fazem os repositories?
    // Fazem o acesso dos métodos ao banco de dados

    private readonly AppDBContext _dBContext;

    public PublishingRepository(AppDBContext dBContext)
    {
        _dBContext  = dBContext;
    }

    public async Task<IEnumerable<Publishing>> GetAll()
    {
        return await _dBContext.Publishers.ToListAsync();
    }
    public async Task<IEnumerable<Publishing>> GetPublishersBooks()
    {
        return await _dBContext.Publishers.Include(p => p.Books).ToListAsync();
    }
    public async Task<Publishing> GetById(int id)
    {
        return await _dBContext.Publishers.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Publishing> Create(Publishing publishing)
    {
        _dBContext.Publishers.Add(publishing);
        await _dBContext.SaveChangesAsync();
        return publishing;
    }
    public async Task<Publishing> Update(Publishing publishing)
    {
        _dBContext.Entry(publishing).State = EntityState.Modified;
        await _dBContext.SaveChangesAsync();
        return publishing;
    }

    public async Task<Publishing> Delete(int id)
    {
        var publishing = await GetById(id);
        _dBContext.Publishers.Remove(publishing);
        await _dBContext.SaveChangesAsync();
        return publishing;
    }

}
