using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Mediator.Types.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeleteUserRequest : IRequest<HandlerResponse<bool>>
    {
        public long Id { get; }

        public DeleteUserRequest(long id)
        {
            Id = id;
        }
    }
}
