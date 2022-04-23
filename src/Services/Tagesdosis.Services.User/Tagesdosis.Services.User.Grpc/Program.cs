using Tagesdosis.Application;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Extensions;
using Tagesdosis.Services.User.Grpc.Services;
using Tagesdosis.Services.User.Identity;

var builder = WebApplication.CreateBuilder(args);

// gRPC configuration
builder.Services.AddGrpc();

// Data layer
builder.AddPersistence();

// MediatR and FluentValidation pipeline configuration
builder.Services.AddApplication(new [] {typeof(AppUser).Assembly, typeof(TokenService).Assembly});
builder.Services.AddTransient<IIdentityService, IdentityService>();

// Identity and security
builder.Services.AddIdentity();
builder.AddSecurity();

var app = builder.Build();

app.EnsureDatabaseCreated();
app.UseRouting();
app.UseSecurity();

// gRPC endpoints configuration
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<RoleService>();
    endpoints.MapGrpcService<TokenService>();
    endpoints.MapGrpcService<UserService>();
});

app.Run();