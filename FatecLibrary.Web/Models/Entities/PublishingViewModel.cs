using System.ComponentModel.DataAnnotations;

namespace FatecLibrary.Web.Models.Entities;

public class PublishingViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Acronym { get; set; }
}
