using LinkHubApp.Components;
using LinkHubApp.Models;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();

var pages = new Dictionary<string, LinkPage>
{
    ["demo"] = new LinkPage(
        BannerUrl: "https://via.placeholder.com/1200x300",
        AvatarUrl: "https://via.placeholder.com/150",
        Title: "Demo User",
        SocialLinks: new List<SocialLink>
        {
            new("home", "https://example.com", "Website"),
            new("favorite", "https://example.com/donate", "Donate")
        },
        Links: new List<LinkItem>
        {
            new("shopping_cart", "https://example.com/product", "Product")
        },
        FooterText: "Generated with love")
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/api/pages/{name}", (string name) =>
    pages.TryGetValue(name, out var page) ? Results.Ok(page) : Results.NotFound());

app.MapPost("/api/pages/{name}", (string name, LinkPage page) =>
{
    pages[name] = page;
    return Results.Ok();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
