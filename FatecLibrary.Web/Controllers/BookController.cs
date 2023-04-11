using FatecLibrary.Web.Models.Entities;
using FatecLibrary.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> Index()
        {
            var result = await _bookService.GetAllBooks();
            if (result is null) return View("Error");
            return View(result);
        }

        // Criar a view CreateBook
        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookService.CreateBook(bookViewModel);

                if (result is null) return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("Error");
            }
            return View(bookViewModel);
        }

        // Criar a view UpdateBook
        [HttpGet]
        public async Task<IActionResult> UpdateBook(int id)
        {
            var result = await _bookService.FindBookById(id);
            if (result is null) return View("Error");
            return View(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookViewModel bookViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookService.UpdateBook(bookViewModel);
                if (result is not null) return RedirectToAction(nameof(Index));
                else
                    return BadRequest("Error");
            }
            return View(bookViewModel);
        }

    }
}
