using CMS.Users.Data;
using CMS.Users.Mediator.Types;
using CMS.Users.Mediator.Types.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, HandlerResponse<bool>>
    {
        private readonly UserDataContext _dataContext;

        public DeleteUserHandler(UserDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HandlerResponse<bool>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            HandlerResponse<bool> response;


            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
            {
                response = new HandlerResponse<bool>(true, "User was not present");
            }
            else
            {
                try
                {
                    _dataContext.Users.Remove(user);
                    await _dataContext.SaveChangesAsync(cancellationToken);

                    response = new HandlerResponse<bool>(true);
                }
                catch (DbException)
                {
                    response = new HandlerResponse<bool>($"Unable to delete User");
                }
            }

            return response;
        }
    }
}
