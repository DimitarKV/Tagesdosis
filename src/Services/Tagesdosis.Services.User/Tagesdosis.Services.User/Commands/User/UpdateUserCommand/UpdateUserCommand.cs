using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.Views;

namespace Tagesdosis.Services.User.Commands.User.UpdateUserCommand;

public class UpdateUserCommand : IRequest<ApiResponse<UserView>>
{
    public string UserName { get; set; }
    public string? NewUserName { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserView>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IMessageSenderFactory _senderFactory;
    private readonly IConfiguration _configuration;

    public UpdateUserCommandHandler(IMapper mapper, IIdentityService identityService, IMessageSenderFactory senderFactory, IConfiguration configuration)
    {
        _mapper = mapper;
        _identityService = identityService;
        _senderFactory = senderFactory;
        _configuration = configuration;
    }

    /// <summary>
    /// Updates the fields of the user which are not empty strings
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse<UserView>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);

        string messageIfUsernameChanged = "";
        if (request.NewUserName is not null)
        {
            user!.UserName = request.NewUserName;
            messageIfUsernameChanged = " Please login again with your new username " + request.NewUserName;
        }

        if (request.Email is not null)
            user!.Email = request.Email;
        
        if (request.CurrentPassword is not null && request.NewPassword is not null)
        {
            var result = await _identityService.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return new ApiResponse<UserView>(null, "An error occurred while updating a user " + request.UserName,
                    result.Errors.Select(e => e.Description));
        }

        user!.UpdatedOn = DateTime.Now;

        var updateResult = await _identityService.UpdateAsync(user);

        if (updateResult.Succeeded)
        {
            if (request.NewUserName is not null)
            {
                SendNotificationAsync(user.Id, request.NewUserName);
            }
            
            return new ApiResponse<UserView>(_mapper.Map<UserView>(user), $"Successfully updated user {request.UserName}.{messageIfUsernameChanged}");
        }

        return new ApiResponse<UserView>(null, "An error occurred while updating a user",
            updateResult.Errors.Select(x => x.Description));
    }
    
    private async void SendNotificationAsync(string id, string userName)
    {
        var @event = new UserCreatedEvent(id, userName);
        var sender = _senderFactory.CreateAzureTopicSender<UserCreatedEvent>(_configuration["AzureServiceBus:Topics:User"]);
        await sender.SendAsync(@event, new MessageMetaData
        {
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now
        }, CancellationToken.None);
    }
}