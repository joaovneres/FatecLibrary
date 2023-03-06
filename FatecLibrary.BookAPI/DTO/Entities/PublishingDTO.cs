using FatecLibrary.BookAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FatecLibrary.BookAPI.DTO.Entities;

public class PublishingDTO
{
    [Required(ErrorMessage = "The Edition is required!")]
    [MinLength(3)]
    [MaxLength(100)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Acronym { get; set; }
    public ICollection<BookDTO>? BooksDTO { get; set; }
}
