namespace FatecLibrary.BookAPI.Models.Entities;

public class Publishing
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Acronym { get; set; }
    public ICollection<Book>? Books { get; set; }
}
