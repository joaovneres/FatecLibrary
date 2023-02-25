using FatecLibrary.BookAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FatecLibrary.BookAPI.Context;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    // aqui definimos o mapeamento dos objetos relacionais BD
    public DbSet<Publishing> Publishers { get; set; }
    public DbSet<Book> Books { get; set; }

    // aqui usamos Fluent API e não Data Annotations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publishing>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Publishing>()
            .Property(p => p.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Publishing>()
            .Property(p => p.Acronym).HasMaxLength(10);

        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Book>()
            .Property(b => b.Title).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Book>()
            .Property(b => b.Price).HasPrecision(8,2);
        modelBuilder.Entity<Book>()
            .Property(b => b.PublicationYear);
        modelBuilder.Entity<Book>()
            .Property(b => b.Edition);
        modelBuilder.Entity<Book>()
            .Property(b => b.ImageURL).HasMaxLength(255);
    
        // relacionamento
        modelBuilder.Entity<Publishing>().HasMany(p => p.Books).WithOne(b => b.Publishing).IsRequired().OnDelete(DeleteBehavior.Cascade);

        //Populando o BD caso com as primeiras editoras
        modelBuilder.Entity<Publishing>().HasData(
            new Publishing
            {
                Id = 1,
                Name = "Alta Books",
                Acronym = "AB",
            },
            new Publishing
            {
                Id = 2,
                Name = "Editora Fatec",
                Acronym = "Fatec",
            });

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 3,
                Title = "Alta",
                Price = 1,
                PublicationYear = 1,
                Edition = 1,
                ImageURL = "asas",
                PublishingId = 1,
            });
    }

}
