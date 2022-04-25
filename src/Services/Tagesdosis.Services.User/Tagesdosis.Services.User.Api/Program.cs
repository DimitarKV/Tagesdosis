using Tagesdosis.Application;
using Tagesdosis.Application.Extensions;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Infrastructure.MessageBrokers.AzureServiceBus;
using Tagesdosis.Infrastructure.ProtectedStorage.AzureKeyVault;
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

var sender = new AzureServiceBusTopicSender<string>("Endpoint=sb://tagesdosis.servicebus.windows.net/;SharedAccessKeyName=ReadWrite;SharedAccessKey=hvkAdmZStj+telkUbag+AidbkV/fCinO0cEbacZIQS0=", "user");
await sender.SendAsync("Davai blyat", new MessageMetaData
{
    CreatedOn = DateTime.Now,
    UpdatedOn = DateTime.Now
}, CancellationToken.None);
/*if (builder.Environment.IsProduction())
{
    builder.AddAzureKeyVault(new AzureKeyVaultOptions
    {
        CertificateThumbprint = builder.Configuration["AzureKeyVault:CertificateThumbprint"],
        ApplicationId = builder.Configuration["AzureKeyVault:ApplicationId"],
        TenantId = builder.Configuration["AzureKeyVault:TenantId"],
        Name = builder.Configuration["AzureKeyVault:Name"]
    });
}*/

app.EnsureDatabaseCreated();
app.UseSecurity();
app.MapControllers();

app.Run();