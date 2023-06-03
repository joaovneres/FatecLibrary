using FatecLibrary.Web.Models.Entities;
using FatecLibrary.Web.Roles;
using FatecLibrary.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FatecLibrary.Web.Controllers;

[Authorize(Roles = Role.Admin)]
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IPublishingService _publishingService;

    public BookController(IBookService bookService, IPublishingService publishingService)
    {
        _bookService = bookService;
        _publishingService = publishingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookViewModel>>> Index()
    {
        var result = await _bookService.GetAllBooks(await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    // Criar a view CreateBook
    [HttpGet]
    public async Task<IActionResult> CreateBook()
    {
        ViewBag.PublishingId = new SelectList(await _publishingService.GetAllPublishers(await GetAccessToken()), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookViewModel bookViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _bookService.CreateBook(bookViewModel, await GetAccessToken());

            if (result is not null) return RedirectToAction(nameof(Index));
        }
        else
            ViewBag.PublishingId = new SelectList(await _publishingService.GetAllPublishers(await GetAccessToken()), "Id", "Name");
        // return BadRequest("Erro");

        return View(bookViewModel);
    }

    // Criar a view UpdateBook
    [HttpGet]
    public async Task<IActionResult> UpdateBook(int id)
    {
        ViewBag.PublishingId = new SelectList(await _publishingService.GetAllPublishers(await GetAccessToken()), "Id", "Name");
        var result = await _bookService.FindBookById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBook(BookViewModel bookViewModel, int id)
    {
        if (ModelState.IsValid)
        {
            if (await _publishingService.FindPublishingById(bookViewModel.PublishingId, await GetAccessToken()) is not null)
            {
                var result = await _bookService.UpdateBook(bookViewModel, await GetAccessToken());
                if (result is not null) return RedirectToAction(nameof(Index));
            }
            else
                return BadRequest("Error");
            // ViewBag.PublishingId = new SelectList(await _publishingService.GetAllPublishers(), "Id", "Name");
        }
        return View(bookViewModel);
    }

    // Criar a view DeleteBook
    [HttpGet]
    public async Task<ActionResult<BookViewModel>> DeleteBook(int id)
    {
        var result = await _bookService.FindBookById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost, ActionName("DeleteBook")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _bookService.DeleteBookById(id, await GetAccessToken());
        if (!result) return View("Error");
        return RedirectToAction("Index");
        // return RedirectToAction(nameof(Index);
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }
}
