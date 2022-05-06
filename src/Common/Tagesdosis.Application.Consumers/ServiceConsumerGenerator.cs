using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Tagesdosis.Application.Consumers
{

    [Generator]
    public class ServiceConsumerGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                //Debugger.Launch();
            }
#endif
            
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxTrees = context.Compilation.SyntaxTrees;
            var handlers = syntaxTrees.Where(x => x.GetText().ToString().Contains("[BaseUrl"));
            
            foreach (var handler in handlers)
            {
                var usingDirectives = handler.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>();
                var usingDirectivesAsText = string.Join("\r\n", usingDirectives);
                var sourceBuilder = new StringBuilder(usingDirectivesAsText);

                var classDeclarationSyntax =
                    handler.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                var className = classDeclarationSyntax.Identifier.ToString();


                var methods = handler
                    .GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();


                var endpoint = "";
                var httpMethod = "";
                
                foreach (var method in methods) //  public partial ApiResponse CreateUser(CreateUserModel model);
                {
                    var attributes = method.AttributeLists.Select(x => x.Attributes);

                    foreach (var attribute in attributes)
                    {
                        foreach (var attributeSyntax in attribute)
                        {
                            var args = attributeSyntax.ArgumentList.Arguments;
                            if (attributeSyntax.Name.ToString() == "Action")
                            {
                                endpoint = attributeSyntax.ArgumentList.Arguments[0].ToString();
                                httpMethod = attributeSyntax.ArgumentList.Arguments[1].ToString().Replace("\"", "");
                            }
                        }
                    }
                }
                
                var namespaceDeclarationSyntax =
                handler.GetRoot().ChildNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
                var namespaceName = GetNamespace(classDeclarationSyntax);

                sourceBuilder.Append($@"
using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
");
                
                sourceBuilder.Append($@"
namespace {namespaceName}
{{

public partial class {className} : IServiceConsumer
{{

private readonly HttpClient _httpClient;
public {className}(IHttpClientFactory factory, IConfiguration configuration)
{{
var clientName = configuration[""Services:User:Client""];
_httpClient = factory.CreateClient(clientName);
}} 


public partial Task<ApiResponse> CreateUser(CreateUserModel model)
{{
return __CreateUser(model);
}}

private async Task<ApiResponse> __CreateUser(CreateUserModel model)
{{
var request = new HttpRequestMessage(HttpMethod.{httpMethod}, {endpoint});
var json = JsonSerializer.Serialize(model);

request.Content = new StringContent(json);
request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(""application/json"");
var response = await _httpClient.SendAsync(request);
var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

return apiResponse;
}}

}}
}}
 ");
                
                
                
                
                //sourceBuilder.Append(@"} }");
                context.AddSource($"{className}.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
            }
        }
        
        static string GetNamespace(BaseTypeDeclarationSyntax syntax)
        {
            // If we don't have a namespace at all we'll return an empty string
            // This accounts for the "default namespace" case
            string nameSpace = string.Empty;
        
            // Get the containing syntax node for the type declaration
            // (could be a nested type, for example)
            SyntaxNode potentialNamespaceParent = syntax.Parent;
        
            // Keep moving "out" of nested classes etc until we get to a namespace
            // or until we run out of parents
            while (potentialNamespaceParent != null &&
                   potentialNamespaceParent is not NamespaceDeclarationSyntax
                   && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
            {
                potentialNamespaceParent = potentialNamespaceParent.Parent;
            }
        
            // Build up the final namespace by looping until we no longer have a namespace declaration
            if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
            {
                // We have a namespace. Use that as the type
                nameSpace = namespaceParent.Name.ToString();
        
                // Keep moving "out" of the namespace declarations until we 
                // run out of nested namespace declarations
                while (true)
                {
                    if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent)
                    {
                        break;
                    }
        
                    // Add the outer namespace as a prefix to the final namespace
                    nameSpace = $"{namespaceParent.Name}.{nameSpace}";
                    namespaceParent = parent;
                }
            }
        
            // return the final namespace
            return nameSpace;
        }
    }
}