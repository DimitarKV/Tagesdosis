namespace Tagesdosis.Infrastructure.ProtectedStorage.AzureKeyVault;

public class AzureKeyVaultOptions
{
    public string Name { get; set; }
    public string TenantId { get; set; }
    public string ApplicationId { get; set; }
    public string CertificateThumbprint { get; set; }
}