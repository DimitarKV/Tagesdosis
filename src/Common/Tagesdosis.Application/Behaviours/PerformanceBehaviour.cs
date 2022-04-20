using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Tagesdosis.Application.Behaviours;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    private const int Timeout = 1000;
    
    // ReSharper disable once ContextualLoggerProblem
    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > Timeout)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Long Running Request: {RequestName} ({ElapsedMilliseconds} milliseconds) {Request}", requestName, elapsedMilliseconds, request);
        }

        return response;
    }
}