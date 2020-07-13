using CMS.Users.Data.Entities;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Mediator.Types.Commands
{
    [ExcludeFromCodeCoverage]
    public class CreateUserRequest : IRequest<HandlerResponse<User>>
    {
        public string Username { get; }

        public CreateUserRequest(string username)
        {
            Username = username;
        }
    }
}
