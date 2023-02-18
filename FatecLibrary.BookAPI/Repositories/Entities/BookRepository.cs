using FatecLibrary.BookAPI.Models.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;

namespace FatecLibrary.BookAPI.Repositories.Entities;

public class BookRepository : IBookRepository
{
    public BookRepository()
    {
    }

    public Task<Book> Create(Book book)
    {
        throw new NotImplementedException();
    }

    public Task<Book> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Book>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> Update(Book book)
    {
        throw new NotImplementedException();
    }
}
