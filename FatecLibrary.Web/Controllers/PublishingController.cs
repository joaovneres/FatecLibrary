using FatecLibrary.Web.Models.Entities;
using FatecLibrary.Web.Roles;
using FatecLibrary.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FatecLibrary.Web.Controllers;

[Authorize(Roles = Role.Admin)]
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
        var result = await _publishingService.GetAllPublishers(await GetAccessToken());
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
            var result = await _publishingService.CreatePublishing(publishingViewModel, await GetAccessToken());

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
        var result = await _publishingService.FindPublishingById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePublishing(PublishingViewModel publishingViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _publishingService.UpdatePublishing(publishingViewModel, await GetAccessToken());
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
        var result = await _publishingService.FindPublishingById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost, ActionName("DeletePublishing")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _publishingService.DeletePublishing(id, await GetAccessToken());
        if (!result) return View("Error");
        return RedirectToAction("Index");
        // return RedirectToAction(nameof(Index));
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }
}
