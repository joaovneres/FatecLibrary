using Microsoft.AspNetCore.Identity;

namespace FatecLibrary.IdentityServer.Data.Entities;

public class ApplicationUser : IdentityUser
{
    // outra opção para tirar o avio de nulo String.Empty
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}
