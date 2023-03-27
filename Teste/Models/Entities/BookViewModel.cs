using System.ComponentModel.DataAnnotations;

namespace FatecLibrary.Web.Models.Entities;

public class BookViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [DisplayFormat(DataFormatString = "{0:C}")]
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int PublicationYear { get; set; }
    [Required]
    public int Edition { get; set; }

    public string? ImageURL { get; set; }
    public string? PublishingName { get; set; }
    [Required]
    [Display(Name = "Publishers")]
    public int PublishingId { get; set; }
}
