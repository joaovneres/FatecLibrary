using FatecLibrary.BookAPI.DTO.Entities;

namespace FatecLibrary.BookAPI.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDTO>> GetAll();
    Task<BookDTO> GetById(int id);
    Task<BookDTO> Create(BookDTO bookDTO);
    Task<BookDTO> Update(BookDTO bookDTO);
    Task<BookDTO> Delete(int id);
}
