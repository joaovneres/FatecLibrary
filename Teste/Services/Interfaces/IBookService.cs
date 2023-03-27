using FatecLibrary.Web.Models.Entities;

namespace FatecLibrary.Web.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllBooks();
    Task<BookViewModel> FindBookById(int id);
    Task<BookViewModel> CreateBook(BookViewModel bookVM);
    Task<BookViewModel> UpdateBook(BookViewModel bookVM);
    Task<bool> DeleteBookById(int id);
}
