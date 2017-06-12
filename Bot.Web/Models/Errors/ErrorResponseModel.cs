using System;
using System.ComponentModel.DataAnnotations;

namespace Bot.Web.Models.Errors
{
    /// <summary>
    /// Standard error response message.
    /// </summary>
    public class ErrorResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponseModel"/> class.
        /// </summary>
        public ErrorResponseModel(ErrorModel error)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponseModel"/> class.
        /// </summary>
        public ErrorResponseModel(Exception ex)
        {
            Error = new ErrorModel(ex ?? throw new ArgumentNullException(nameof(ex)));
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        [Required]
        public ErrorModel Error { get; }

        /// <inheritdoc />
        public override string ToString() => Error.ToString();
    }
}