using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Tagesdosis.Application.Generators.ServiceConsumers;
using Tagesdosis.Application.Generators.ServiceConsumers.Extensions;

namespace Tagesdosis.Application.Generators
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

                var namespaceName = classDeclarationSyntax.GetNamespace();

                var builder = ServiceConsumerSourceBuilder
                    .Create()
                    .DeclareClass(namespaceName, className);

                var methods = handler
                    .GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();

                foreach (var method in methods) //  public partial ApiResponse CreateUser(CreateUserModel model);
                {
                    var endpoint = "";
                    var httpMethod = "";
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

                    var returnTypeSyntax = method.ReturnType as GenericNameSyntax;
                    var returnType = returnTypeSyntax.TypeArgumentList.Arguments.First().ToString();
                    var parameter = method.ParameterList.Parameters[0].Type.ToString();
                    builder.DeclareMethod(method.Identifier.ToString(), returnType,
                        parameter, httpMethod, endpoint);
                }
                
                var src = builder.BuildSource();
                context.AddSource($"{className}.g.cs", SourceText.From(src, Encoding.UTF8));
            }
        }
    }
}