using MediatR;
using Tagesdosis.Domain.Types;

namespace Tagesdosis.Services.User.Commands.Role.AddUserToRoleCommand;

public class AddUserToRoleCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }
    public string Role { get; set; }
}

public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, ApiResponse>
{
    public Task<ApiResponse> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}