using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tagesdosis.Application;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Data.Persistence;
using Tagesdosis.Services.User.Grpc.Services;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(new [] {typeof(AppUser).Assembly, typeof(TokenService).Assembly});

builder.Services.AddTransient<IIdentityService, IdentityService>();

var databaseConnString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(databaseConnString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSecurity(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<UserDbContext>();
    db!.Database.EnsureCreated();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GreeterService>();
    endpoints.MapGrpcService<RoleService>();
    endpoints.MapGrpcService<TokenService>();
});

// Configure the HTTP request pipeline.


app.Run();