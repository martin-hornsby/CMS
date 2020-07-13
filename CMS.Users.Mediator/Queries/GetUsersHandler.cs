using CMS.Users.Data;
using CMS.Users.Data.Entities;
using CMS.Users.Mediator.Types;
using CMS.Users.Mediator.Types.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.Queries
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, HandlerResponse<IEnumerable<User>>>
    {
        private readonly UserDataContext _dataContext;

        public GetUsersHandler(UserDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HandlerResponse<IEnumerable<User>>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            HandlerResponse<IEnumerable<User>> response;

            try
            {
                var users = await _dataContext.Users.AsNoTracking().OrderBy(u => u.Id).ToListAsync();

                response = new HandlerResponse<IEnumerable<User>>(users);
            }
            catch (DbException)
            {
                response = new HandlerResponse<IEnumerable<User>>($"Unable to get Users");
            }

            return response;
        }
    }
}
