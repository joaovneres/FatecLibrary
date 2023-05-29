using FatecLibrary.Web.Services.Entities;
using FatecLibrary.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(options =>
{
    options.DefaultScheme = "Coockies";
    options.DefaultChallengeScheme = "oidc"; // tipo de autentica��o que ser� utilizado
})
    .AddCookie("Cookies", c =>
    {
        c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        // caso o acesso seja negado, ele redireciona para a p�gina de acesso negado
        c.Events = new CookieAuthenticationEvents()
        {
            OnRedirectToAccessDenied = (context) =>
            {
                context.HttpContext.Response.Redirect(builder.Configuration["ServiceUri:IdentityServer"] + "/Account/AccessDenied");
                return Task.CompletedTask;
            }
        };
    })
    .AddOpenIdConnect("oidc", options =>
    {
        // quando o usu�rio clicar no cancel do login
        options.Events.OnRemoteFailure = context =>
        {
            context.Response.Redirect("/");
            context.HandleResponse();

            return Task.FromResult(0);
        };

        options.Authority = builder.Configuration["ServiceUri:IdentityServer"]; // appsettings.json
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "fateclibrary";
        // O secret aqui est� pegando do arquivo appsettings.json
        // mas inicialmente foi criado l� na classe do IdentityConfiguration
        options.ClientSecret = builder.Configuration["Client:Secret"];
        options.ResponseType = "code";
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.ClaimActions.MapJsonKey("sub", "sub", "sub");
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("fateclibrary"); // escopo foi definido l� no Identity Configuration
        options.SaveTokens = true;
    });

builder.Services.AddHttpClient("BookAPI", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:BookAPI"]);
});


// Inje��o de dep�ndencia
builder.Services.AddScoped<IPublishingService, PublishingService>();
builder.Services.AddScoped<IBookService, BookService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
