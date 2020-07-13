using CMS.Users.Data;
using CMS.Users.Mediator.Types;
using CMS.Users.Mediator.Types.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.Commands
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, HandlerResponse<bool>>
    {
        private readonly UserDataContext _dataContext;

        public UpdateUserHandler(UserDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HandlerResponse<bool>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            HandlerResponse<bool> response;

            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
            {
                response = new HandlerResponse<bool>("Unable to find User");
            }
            else
            {
                var existingUserName = await _dataContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

                if (existingUserName != null)
                {
                    response = new HandlerResponse<bool>($"Please supply an unique username");
                }
                else
                {
                    try
                    {
                        user.Username = request.Username;
                        await _dataContext.SaveChangesAsync(cancellationToken);

                        response = new HandlerResponse<bool>(true);
                    }
                    catch (DbException)
                    {
                        response = new HandlerResponse<bool>($"Unable to update User");
                    }
                }
            }

            return response;
        }
    }
}
