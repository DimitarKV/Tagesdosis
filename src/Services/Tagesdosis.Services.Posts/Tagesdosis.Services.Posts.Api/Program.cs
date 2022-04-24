using Tagesdosis.Application;
using Tagesdosis.Application.Extensions;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Persistance;
using Tagesdosis.Services.Posts.Data.Persistance.Interfaces;
using Tagesdosis.Services.Posts.Data.Repositories;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;
using Tagesdosis.Services.Posts.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPersistence();
builder.Services.AddApplication(new [] {typeof(Post).Assembly});
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IPostDbContext, PostDbContext>();
builder.AddSecurity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnsureDatabaseCreated();
app.UseSecurity();
app.MapControllers();

app.Run();