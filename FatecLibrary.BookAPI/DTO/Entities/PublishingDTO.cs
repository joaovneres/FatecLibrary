using FatecLibrary.BookAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FatecLibrary.BookAPI.DTO.Entities;

public class PublishingDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "The name is required!")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }
    public string? Acronym { get; set; }
    public ICollection<BookDTO>? BooksDTO { get; set; }
}
