using FatecLibrary.IdentityServer.Configuration;
using FatecLibrary.IdentityServer.Data.Context;
using FatecLibrary.IdentityServer.Data.Entities;
using FatecLibrary.IdentityServer.SeedDataBase.Entities;
using FatecLibrary.IdentityServer.SeedDataBase.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//pegando a string de conex�o
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Usar para que o Entity Framework crie nossas tabelas no banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection))
    );

// adionando a refer�ncia ao Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

// configurando o Identity

var builderIdentityServer = builder.Services.AddIdentityServer(options =>
{
    // configurando os eventos do identity em caso de ocorr�ncia

    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(
    IdentityConfiguration.IdentityResources)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>();

builderIdentityServer.AddDeveloperSigningCredential();

// para criar os perfis de usu�rios eu crio o met�do
// SeedDataBaseIdentityServer
builder.Services.AddScoped<IDatabaseInitializer, DatabaseIdentityServerInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer(); // adicionar aqui
app.UseAuthorization();

// chamando o met�do SeedDatabaseIdentityServer
SeedDatabaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabaseIdentityServer(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        // atrav�s dessa inst�ncia do servi�o de IDatabaseInitializer
        // eu posso chamar os met�dos para cirar as regras e os perfis dos usu�rios
        var initRoleUsers = serviceScope.ServiceProvider
            .GetService<IDatabaseInitializer>();

        initRoleUsers.InitializeSeedUsers();
        initRoleUsers.InitializeSeedRoles();
    }
}