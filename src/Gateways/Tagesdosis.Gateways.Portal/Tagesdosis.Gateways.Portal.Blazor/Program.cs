using Microsoft.AspNetCore.Components.Authorization;
using Tagesdosis.Gateways.Portal.Providers;
using Tagesdosis.Gateways.Portal.Services.Post;
using Tagesdosis.Gateways.Portal.Services.Post.Interfaces;
using Tagesdosis.Gateways.Portal.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(opt => opt.RootDirectory = "/Features");
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<UserServiceConsumer>();
builder.Services.AddScoped<TokenAuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();

builder.Services.AddHttpClient(builder.Configuration["Services:User:Client"], client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:User:Endpoint"]);
});

builder.Services.AddHttpClient(builder.Configuration["Services:Post:Client"], client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Post:Endpoint"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();