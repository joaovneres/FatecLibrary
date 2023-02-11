namespace FatecLibrary.BookAPI.Models.Entities;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public int PublicationYear { get; set; }
    public int Edition { get; set; }
    public string? ImageURL { get; set; }


    public Publishing? Publing { get; set; }
    public int PublishingId { get; set; }
}
