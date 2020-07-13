using CMS.Users.Mediator.Types.Commands;
using CMS.Users.Mediator.Types.Queries;
using CMS.Users.WebApi.Requests.Users;
using CMS.Users.WebApi.Validators.UserController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CMS.Users.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;
        private readonly CreateUserValidator _createValidator;
        private readonly UpdateUserValidator _updateValidator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator, CreateUserValidator createValidator, UpdateUserValidator updateValidator)
        {
            _logger = logger;
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        /// <summary>
        ///     Create User
        /// </summary>
        /// <returns>The newly created User</returns>
        /// <response code="200">Returns the newly created User</response>
        /// <response code="400">Validation failed</response>
        [HttpPost]
        [ProducesResponseType(typeof(Data.Entities.User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser request)
        {
            _logger.LogDebug($"Received request to Create User with username: {request?.Username}");
            var validationResult = _createValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(",", validationResult.Errors.Select(err => err.ErrorMessage));
                _logger.LogWarning($"CreateUser request failed validation | Reason : {errorMessage}");
                return BadRequest(errorMessage);
            }
            else
            {
                var mediatorRequest = new CreateUserRequest(request?.Username);
                var mediatorResponse = await _mediator.Send(mediatorRequest);

                if (mediatorResponse.Success)
                {
                    var user = mediatorResponse.Value;
                    _logger.LogInformation($"Created User with username: {user.Username}");
                    return Created($"users/{user.Id}", user);
                }
                else
                {
                    var errorMessage = mediatorResponse.Message;
                    _logger.LogWarning($"Unable to create User with username: {mediatorRequest.Username} | Reason : {errorMessage}");
                    return BadRequest(errorMessage);
                }
            }
        }

        /// <summary>
        ///     Get all Users
        /// </summary>
        /// <returns>All Users within the system</returns>
        /// <response code="200">Returns the list of all Users</response>
        /// <response code="400">Unable to retrieve the list of Users</response>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Data.Entities.User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogDebug($"Received request to get all Users");
            var mediatorRequest = new GetUsersRequest();
            var mediatorResponse = await _mediator.Send(mediatorRequest);

            if (mediatorResponse.Success)
            {
                _logger.LogInformation($"Returning list of all Users");
                return Ok();
            }
            else
            {
                var errorMessage = mediatorResponse.Message;
                _logger.LogWarning($"Unable to retrieve list of all Users | Reason : {errorMessage}");
                return BadRequest(errorMessage);
            }
        }

        /// <summary>
        ///     Updates the requested User
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>All Users within the system</returns>
        /// <response code="200">Successfully updated the User</response>
        /// <response code="400">Validation failed</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUser request)
        {
            _logger.LogDebug($"Received request to Update User Id: {id}");
            var validationResult = _updateValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(",", validationResult.Errors.Select(err => err.ErrorMessage));
                _logger.LogWarning($"UpdateUser request failed validation | Reason : {errorMessage}");
                return BadRequest(errorMessage);
            }
            else
            {
                var mediatorRequest = new UpdateUserRequest(id, request?.Username);
                var mediatorResponse = await _mediator.Send(mediatorRequest);

                if (mediatorResponse.Success)
                {
                    _logger.LogInformation($"Updated User Id {mediatorRequest.Id} with username: {mediatorRequest.Username}");
                    return Ok();
                }
                else
                {
                    var errorMessage = mediatorResponse.Message;
                    _logger.LogWarning($"Unable to update User Id {mediatorRequest.Id} | Reason : {errorMessage}");
                    return BadRequest(errorMessage);
                }
            }
        }

        /// <summary>
        ///     Deletes the requested User
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <response code="200">Successfully deleted the User</response>
        /// <response code="400">Unable to delete the User</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(long id)
        {
            _logger.LogDebug($"Received request to Delete User Id: {id}");
            var mediatorRequest = new DeleteUserRequest(id);
            var mediatorResponse = await _mediator.Send(mediatorRequest);

            if (mediatorResponse.Success)
            {
                _logger.LogInformation($"Deleted User Id {mediatorRequest.Id}");
                return Ok();
            }
            else
            {
                var errorMessage = mediatorResponse.Message;
                _logger.LogWarning($"Unable to delete User Id {mediatorRequest.Id}");
                return BadRequest(errorMessage);
            }
        }
    }
}
