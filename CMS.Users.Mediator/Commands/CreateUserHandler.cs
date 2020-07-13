using CMS.Users.Data;
using CMS.Users.Data.Entities;
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
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, HandlerResponse<User>>
    {
        private readonly UserDataContext _dataContext;

        public CreateUserHandler(UserDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HandlerResponse<User>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            HandlerResponse<User> response;

            var existingUser = await _dataContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

            if (existingUser != null)
            {
                response = new HandlerResponse<User>($"Please supply an unique username");
            }
            else
            {
                var userToCreate = new User { Username = request.Username };

                try
                {
                    await _dataContext.Users.AddAsync(userToCreate, cancellationToken);
                    await _dataContext.SaveChangesAsync(cancellationToken);

                    var user = await _dataContext.Users.AsNoTracking().SingleAsync(u => u.Id == userToCreate.Id, cancellationToken);
                    response = new HandlerResponse<User>(user);
                }
                catch (DbException)
                {
                    response = new HandlerResponse<User>($"Unable to create User");
                }
            }

            return response;
        }
    }
}
