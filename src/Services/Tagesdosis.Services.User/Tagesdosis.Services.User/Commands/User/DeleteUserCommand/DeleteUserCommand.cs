using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.DeleteUserCommand;

public class DeleteUserCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }

    public DeleteUserCommand()
    {
        
    }

    public DeleteUserCommand(string userName)
    {
        UserName = userName;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IMessageSenderFactory _senderFactory;
    private readonly IConfiguration _configuration;

    public DeleteUserCommandHandler(IIdentityService identityService, IMessageSenderFactory senderFactory, IConfiguration configuration)
    {
        _identityService = identityService;
        _senderFactory = senderFactory;
        _configuration = configuration;
    }

    /// <summary>
    /// Deletes the user with the given UserName
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
        var result = await _identityService.DeleteAsync(user!);
        if (result.Succeeded)
        {
            SendNotificationAsync(user!.Id);
            return new ApiResponse("Deleted user " + request.UserName);
        }
        return new ApiResponse("Unable to delete user with username " + request.UserName);
    }
    
    private async void SendNotificationAsync(string id)
    {
        var @event = new UserDeletedEvent(id);
        var sender = _senderFactory.CreateAzureTopicSender<UserDeletedEvent>(_configuration["AzureServiceBus:Topics:User"]);
        await sender.SendAsync(@event, new MessageMetaData
        {
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now
        }, CancellationToken.None);
    }
}