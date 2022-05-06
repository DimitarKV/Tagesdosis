using System.Text;

namespace Tagesdosis.Application.Generators.ServiceConsumers
{
    public interface IBuildingStage
    {
        public string BuildSource();
    }
    
    public interface IMethodDeclarationStage : IBuildingStage
    {
        public IMethodDeclarationStage DeclareMethod(string name, string returnType, string model,
            string httpMethod, string endpoint);
    }
    
    public interface IClassDeclarationStage
    {
        public IMethodDeclarationStage DeclareClass(string namespaceName, string className);
    }
    
    public class ServiceConsumerSourceBuilder : IClassDeclarationStage, IMethodDeclarationStage
    {
        private readonly StringBuilder _builder;
        private ServiceConsumerSourceBuilder()
        {
            _builder = new StringBuilder();
        }

        public static IClassDeclarationStage Create()
        {
            return new ServiceConsumerSourceBuilder();
        }

        public IMethodDeclarationStage DeclareClass(string namespaceName, string className)
        {
            _builder.Append($@"
using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Tagesdosis.Domain.Types;
using  Tagesdosis.Application.Infrastructure.ServiceConsumers;
");
            
            _builder.Append($@"
namespace {namespaceName}
{{

public partial class {className} : ServiceConsumerBase
{{

private readonly HttpClient _httpClient;
public {className}(IHttpClientFactory factory, IConfiguration configuration)
{{
var clientName = configuration[""Services:User:Client""];
_httpClient = factory.CreateClient(clientName);
}} 
");
            return this;
        }

        public IMethodDeclarationStage DeclareMethod(string name, string returnType, string model,
            string httpMethod, string endpoint)
        {

            _builder.Append($@"
public partial Task<{returnType}> {name}({model} model)
{{
return __{name}(model);
}}

private async Task<{returnType}> __{name}({model} model)
{{
var request = new HttpRequestMessage(HttpMethod.{httpMethod}, {endpoint});
var json = JsonSerializer.Serialize(model);

request.Content = new StringContent(json);
request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(""application/json"");
var response = await _httpClient.SendAsync(request);
var responseContent = await response.Content.ReadFromJsonAsync<{returnType}>();

return responseContent;
}}");
            
            return this;
        }

        public string BuildSource()
        {
            _builder.Append("}}");
            return _builder.ToString();
        }
    }
}