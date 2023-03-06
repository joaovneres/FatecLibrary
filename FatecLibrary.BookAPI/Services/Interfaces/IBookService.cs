using FatecLibrary.BookAPI.DTO.Entities;

namespace FatecLibrary.BookAPI.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDTO>> GetAll();
    Task<BookDTO> GetById(int id);
    Task Create(BookDTO bookDTO);
    Task Update(BookDTO bookDTO);
    Task Remove(int id);
}
