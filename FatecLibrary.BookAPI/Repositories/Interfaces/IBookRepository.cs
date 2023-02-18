using FatecLibrary.BookAPI.Models.Entities;

namespace FatecLibrary.BookAPI.Repositories.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book> GetById(int id);
    Task<Book> Create(Book book);
    Task<Book> Update(Book book);
    Task<Book> Delete(int id);

}
