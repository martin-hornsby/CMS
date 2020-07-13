using CMS.Users.Data.Entities;
using MediatR;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Mediator.Types.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetUsersRequest : IRequest<HandlerResponse<IEnumerable<User>>>
    {
        public GetUsersRequest() { }
    }
}
