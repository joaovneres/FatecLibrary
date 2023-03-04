using AutoMapper;
using FatecLibrary.BookAPI.DTO.Entities;
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

    public Task<BookDTO> Create(BookDTO bookDTO)
    {
        throw new NotImplementedException();
    }

    public Task<BookDTO> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BookDTO> Update(BookDTO bookDTO)
    {
        throw new NotImplementedException();
    }
}
