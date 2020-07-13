using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Mediator.Types.Commands
{
    [ExcludeFromCodeCoverage]
    public class UpdateUserRequest : IRequest<HandlerResponse<bool>>
    {
        public long Id { get; }
        public string Username { get; }

        public UpdateUserRequest(long id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
