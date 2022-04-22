using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Application.Infrastructure.ProtectedStorage;

namespace Tagesdosis.Infrastructure.ProtectedStorage.AzureKeyVault;

public static class AzureKeyVaultExtensions
{
    public static void AddAzureKeyVault(this WebApplicationBuilder builder, AzureKeyVaultOptions options)
    {
        using var x509Store = new X509Store(StoreLocation.CurrentUser);

        x509Store.Open(OpenFlags.ReadOnly);

        var x509Certificate = x509Store.Certificates
            .Find(
                X509FindType.FindByThumbprint,
                options.CertificateThumbprint,
                validOnly: false).OfType<X509Certificate2>()
            .SingleOrDefault();

        builder.Configuration.AddAzureKeyVault(
            new Uri($"https://{options.Name}.vault.azure.net/"),
            new ClientCertificateCredential(
                options.TenantId,
                options.ApplicationId,
                x509Certificate));

        builder.Services.AddTransient<IProtectedStorage, AzureKeyVault>(storage => new AzureKeyVault(builder.Configuration));
    }
}