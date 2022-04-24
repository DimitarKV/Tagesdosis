using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Services.Posts.Data.Persistance;

namespace Tagesdosis.Services.Posts.Extensions;

public static class DataExtensions
{
    public static void AddPersistence(this WebApplicationBuilder builder, string stringName = "Database")
    {
        var connectionString = builder.Configuration.GetConnectionString(stringName);
        builder.Services.AddDbContext<PostDbContext>(o => o.UseSqlServer(connectionString));
    }

    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<PostDbContext>();
        db!.Database.EnsureCreated();
    }
}