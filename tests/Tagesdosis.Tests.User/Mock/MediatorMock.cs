using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Moq;

namespace Tagesdosis.Tests.User.Mock;

public static class MediatorMock
{
    public static Mock<IMediator> Create<TResponse, TRequest, THandler, TValidator>(THandler handler, TResponse invalid) 
        where TRequest : IRequest<TResponse>, new()
        where THandler : IRequestHandler<TRequest, TResponse>
        where TValidator : AbstractValidator<TRequest>, new()
    {
        var mediator = new Mock<IMediator>();

        var pipeline = TResponse (TRequest request) =>
        {
            var validator = new TValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
                return invalid;
            
            var response = handler.Handle(request, CancellationToken.None);
            response.Wait();

            return response.Result;
        };
        
        mediator.Setup(x => x.Send(It.IsAny<TRequest>(), CancellationToken.None))
            .Returns((TRequest request, CancellationToken token) => Task.FromResult(pipeline(request)));

        return mediator;
    }
}