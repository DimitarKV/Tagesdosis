using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tagesdosis.Application;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Data.Persistence;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(new [] {typeof(AppUser).Assembly});
builder.Services.AddAutoMapper(typeof(AppUser).Assembly);
builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddTransient<IIdentityService, IdentityService>();

var databaseConnString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(databaseConnString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<UserDbContext>();
    db!.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();