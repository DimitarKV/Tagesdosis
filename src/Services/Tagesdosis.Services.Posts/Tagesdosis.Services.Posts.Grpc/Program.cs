using Microsoft.Extensions.DependencyInjection.Extensions;
using Tagesdosis.Application;
using Tagesdosis.Application.Extensions;
using Tagesdosis.Services.Posts.Data.Persistance;
using Tagesdosis.Services.Posts.Data.Persistance.Interfaces;
using Tagesdosis.Services.Posts.Data.Repositories;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;
using Tagesdosis.Services.Posts.Extensions;
using Tagesdosis.Services.Posts.Grpc.Services;
using Tagesdosis.Services.Posts.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// gRPC configuration
builder.Services.AddGrpc();

//Data layer
builder.AddPersistence();

// MediatR and FluentValidation pipeline configuration
builder.Services.AddApplication(new[] {typeof(PostService).Assembly, typeof(Post).Assembly});
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IPostDbContext, PostDbContext>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.AddSecurity();

var app = builder.Build();

app.EnsureDatabaseCreated();
app.UseRouting();
app.UseSecurity();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<PostService>();
});

app.Run();