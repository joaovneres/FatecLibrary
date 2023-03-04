using AutoMapper;
using FatecLibrary.BookAPI.DTO.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;
using FatecLibrary.BookAPI.Services.Interfaces;

namespace FatecLibrary.BookAPI.Services.Entities;

public class PublishingService : IPublishingService
{

    // o que os services fazem?
    // eles fazem as chamadas dos metódos que realizarão as operações
    // no banco de dados, ou seja, os repositories

    private readonly IPublishingRepository _publishingRepository;
    private readonly IMapper _mapper;

    public PublishingService(IPublishingRepository publishingRepository, IMapper mapper)
    {
        _publishingRepository = publishingRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PublishingDTO>> GetAll()
    {
        var publishers = await _publishingRepository.GetAll();
        return _mapper.Map<IEnumerable<PublishingDTO>>(publishers);
    }
    public async Task<IEnumerable<PublishingDTO>> GetPublishersBooks()
    {
        var publishers = await _publishingRepository.GetPublishersBooks();
        return _mapper.Map<IEnumerable<PublishingDTO>>(publishers);
    }
    public async Task<PublishingDTO> GetById(int id)
    {
        var publishing = await _publishingRepository.GetById(id);
        return _mapper.Map<PublishingDTO>(publishing);
    }

    public Task Create(PublishingDTO publishingDTO)
    {
        throw new NotImplementedException();
    }



    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(PublishingDTO publishingDTO)
    {
        throw new NotImplementedException();
    }
}
