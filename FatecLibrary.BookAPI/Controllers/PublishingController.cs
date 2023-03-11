using FatecLibrary.BookAPI.DTO.Entities;
using FatecLibrary.BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecLibrary.BookAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishingController : Controller
{
    private readonly IPublishingService _publishingService;

    public PublishingController(IPublishingService publishingService)
    {
        _publishingService = publishingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublishingDTO>>> Get()
    {
        var publishingDTO = await _publishingService.GetAll();
        if(publishingDTO is null) return NotFound("Publishers not found!");
        return Ok(publishingDTO);
    }

    [HttpGet("books")]
    public async Task<ActionResult<IEnumerable<PublishingDTO>>> GetPublishersBook()
    {
        var publishingDTO = await _publishingService.GetPublishersBooks();
        if (publishingDTO is null) return NotFound("Publishers not found!");
        return Ok(publishingDTO);
    }

    [HttpGet("{id: int}", Name = "GetPublishing")]
    public async Task<ActionResult<PublishingDTO>> Get(int id)
    {
        var publishingDTO = await _publishingService.GetById(id);
        if (publishingDTO is null) return NotFound("Publishing not found!");
        return Ok(publishingDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PublishingDTO publishingDTO)
    {
        if(publishingDTO is null) return BadRequest("Invalid data!");
        await _publishingService.Create(publishingDTO);
        return new CreatedAtRouteResult("GetPublishing", new {id = publishingDTO.Id}, publishingDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] PublishingDTO publishingDTO)
    {
        if (publishingDTO is null) return BadRequest("Invalid data!");
        await _publishingService.Update(publishingDTO);
        return Ok(publishingDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PublishingDTO>> Delete(int id)
    {
        var publishingDTO = await _publishingService.GetById(id);
        if (publishingDTO is null) return NotFound("Publishing not found!");
        await _publishingService.Remove(id);
        return Ok(publishingDTO);
    }
    
}
