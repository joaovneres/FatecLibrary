using AutoMapper;
using FatecLibrary.BookAPI.DTO.Entities;
using FatecLibrary.BookAPI.Models.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;
using FatecLibrary.BookAPI.Services.Interfaces;

namespace FatecLibrary.BookAPI.Services.Entities;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<BookDTO>> GetAll()
    {
        var books = await _bookRepository.GetAll();
        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }
    public async Task<BookDTO> GetById(int id)
    {
        var book = await _bookRepository.GetById(id);
        return _mapper.Map<BookDTO>(book);
    }
    public async Task Create(BookDTO bookDTO)
    {
        var book = _mapper.Map<Book>(bookDTO);
        await _bookRepository.Create(book);
        bookDTO.Id = bookDTO.Id;
    }
    public async Task Update(BookDTO bookDTO)
    {
        var book = _mapper.Map<Book>(bookDTO);
        await _bookRepository.Update(book);
    }

    public async Task Remove(int id)
    {
        var book = await _bookRepository.GetById(id);
        await _bookRepository.Delete(book.Id);
    }
}
