using FatecLibrary.BookAPI.Context;
using FatecLibrary.BookAPI.Models.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecLibrary.BookAPI.Repositories.Entities;

public class BookRepository : IBookRepository
{

    private readonly AppDBContext _dbContext;
    public BookRepository(AppDBContext dBContext)
    {
        _dbContext = dBContext;
    }
    public async Task<IEnumerable<Book>> GetAll()
    {
        return await _dbContext.Books.ToListAsync();
    }
    public async Task<Book> GetById(int id)
    {
        return await _dbContext.Books.Include(p => p.Id == id).Where(b => b.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Book> Create(Book book)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }
    public async Task<Book> Update(Book book)
    {
        _dbContext.Entry(book).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<Book> Delete(int id)
    {
        var book = await GetById(id);
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

}
