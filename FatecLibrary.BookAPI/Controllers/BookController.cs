using FatecLibrary.BookAPI.DTO.Entities;
using FatecLibrary.BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecLibrary.BookAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> Get()
    {
        var bookDTO = await _bookService.GetAll();
        if (bookDTO is null) return NotFound("Books not found!");
        return Ok(bookDTO);
    }

    [HttpGet("{id: int}", Name = "GetBook")]
    public async Task<ActionResult<BookDTO>> Get(int id)
    {
        var bookDTO = await _bookService.GetById(id);
        if (bookDTO is null) return NotFound("Book not found!");
        return Ok(bookDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] BookDTO bookDTO)
    {
        if (bookDTO is null) return BadRequest("Invalid data!");
        await _bookService.Create(bookDTO);
        return new CreatedAtRouteResult("GetBook", new { id = bookDTO.Id }, bookDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] BookDTO bookDTO)
    {
        if (bookDTO is null) return BadRequest("Invalid data!");
        await _bookService.Update(bookDTO);
        return Ok(bookDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BookDTO>> Delete(int id)
    {
        var bookDTO = await _bookService.GetById(id);
        if (bookDTO is null) return NotFound("Book not found!");
        await _bookService.Remove(id);
        return Ok(bookDTO);
    }
}
