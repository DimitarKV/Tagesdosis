using System.Reflection;
using FluentValidation;

namespace Tagesdosis.Tests.Common.AssemblyScanners;

public class ValidatorsScanner<T>
{
    public (List<object> Instances, List<MethodInfo> Methods) Scan(Assembly assembly)
    {
        var validators = assembly
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(AbstractValidator<T>)))
            .Select(Activator.CreateInstance)
            .ToList();

        var methods = validators
            .Select(t => t!.GetType().GetMethod(nameof(AbstractValidator<T>.Validate), new[] {typeof(T)}))
            .ToList();

        return new (validators!, methods!);
    }
}