using Tagesdosis.Application;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Extensions;
using Tagesdosis.Services.User.Identity;

var builder = WebApplication.CreateBuilder(args);

// Web API Controllers and Swagger/OpenApi configuration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Data layer
builder.AddPersistence();

// MediatR and FluentValidation pipeline configuration
builder.Services.AddApplication(new [] {typeof(AppUser).Assembly});
builder.Services.AddTransient<IIdentityService, IdentityService>();

// Identity and security
builder.Services.AddIdentity();
builder.AddSecurity();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnsureDatabaseCreated();
app.UseSecurity();
app.MapControllers();

app.Run();