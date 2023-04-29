using FatecLibrary.Web.Models.Entities;
using FatecLibrary.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecLibrary.Web.Controllers;

public class PublishingController : Controller
{
    private readonly IPublishingService _publishingService;

    public PublishingController(IPublishingService publishingService)
    {
        _publishingService = publishingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublishingViewModel>>> Index()
    {
        var result = await _publishingService.GetAllPublishers();
        if (result is null) return View("Error");
        return View(result);
    }

    // Criar a view CreatePublishing
    [HttpGet]
    public async Task<IActionResult> CreatePublishing()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublishing(PublishingViewModel publishingViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _publishingService.CreatePublishing(publishingViewModel);

            if (result is not null) return RedirectToAction(nameof(Index));
            else
                return BadRequest("Error");
        }
        return View(publishingViewModel);
    }

    // Criar a view UpdatePublishing
    [HttpGet]
    public async Task<IActionResult> UpdatePublishing(int id)
    {
        var result = await _publishingService.FindPublishingById(id);
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePublishing(PublishingViewModel publishingViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _publishingService.UpdatePublishing(publishingViewModel);
            if (result is not null) return RedirectToAction(nameof(Index));
            else
                return BadRequest("Error");
        }
        return View(publishingViewModel);
    }


    // Criar a view DeletePublishing
    [HttpGet]
    public async Task<ActionResult<PublishingViewModel>> DeletePublishing(int id)
    {
        var result = await _publishingService.FindPublishingById(id);
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost, ActionName("DeletePublishing")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _publishingService.DeletePublishing(id);
        if(!result) return View("Error");
        return RedirectToAction("Index");
        // return RedirectToAction(nameof(Index));
    }
}
