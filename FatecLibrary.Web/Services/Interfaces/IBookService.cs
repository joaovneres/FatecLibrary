using FatecLibrary.Web.Models.Entities;

namespace FatecLibrary.Web.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllBooks(string token);
    Task<BookViewModel> FindBookById(int id, string token);
    Task<BookViewModel> CreateBook(BookViewModel bookVM, string token);
    Task<BookViewModel> UpdateBook(BookViewModel bookVM, string token);
    Task<bool> DeleteBookById(int id, string token);
}
