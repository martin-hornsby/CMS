using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Mediator.Types
{
    [ExcludeFromCodeCoverage]
    public class HandlerResponse<TResponseType>
    {
        public TResponseType Value { get; }
        public bool Success { get; }
        public string Message { get; }

        /// <summary>
        /// Constructor to be used when successful
        /// </summary>
        /// <param name="value">An instance of <typeparamref name="TResponseType"/> with the response output of the handler</param>
        /// <param name="message">Optional - any additional information to be provided to the caller</param>
        public HandlerResponse(TResponseType value, string message = null)
        {
            Value = value;
            Success = true;
            Message = message;
        }

        /// <summary>
        /// Constructor to be used when error occurs
        /// </summary>
        /// <param name="errorMessage">The error message which is to be provided to the caller</param>
        public HandlerResponse(string errorMessage)
        {
            Value = default(TResponseType);
            Success = false;
            Message = errorMessage;
        }

        /// <summary>
        /// Constructor to be used when multiple errors occurs
        /// </summary>
        /// <param name="errorMessage">The error messages which are to be provided to the caller</param>
        public HandlerResponse(IEnumerable<string> errorMessages)
        {
            Value = default(TResponseType);
            Success = false;
            Message = string.Join(",", errorMessages);
        }
    }
}
