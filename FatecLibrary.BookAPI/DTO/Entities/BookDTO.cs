using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FatecLibrary.BookAPI.DTO.Entities;

public class BookDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Title is required!")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Title { get; set; }

    [Required(ErrorMessage = "The Price is required!")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Publication Year is required!")]
    public int PublicationYear { get; set; }

    [Required(ErrorMessage = "The Edition is required!")]
    public int Edition { get; set; }

    [Required(ErrorMessage = "The Edition is required!")]
    public string? ImageURL { get; set; }


    [JsonIgnore]
    public PublishingDTO? PublishingDTO { get; set; }
    public int PublishingId { get; set; }
    public string? PublishingName { get; set; }
}
