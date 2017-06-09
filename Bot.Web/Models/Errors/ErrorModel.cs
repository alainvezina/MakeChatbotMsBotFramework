using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bot.Web.Models.Errors
{
    /// <summary>
    /// Information about an error.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorModel"/> class.
        /// </summary>
        public ErrorModel(string code, string message)
        {
            Code = code;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorModel"/> class.
        /// </summary>
        public ErrorModel(Exception ex, string defaultCode = ErrorCode.InternalServerError)
        {
            switch (ex)
            {
                case ValidationException validationException when validationException.ValidationResult != null:
                    Code = ErrorCode.ValidationError;
                    Message = validationException.ValidationResult.ErrorMessage;
                    Target = validationException.ValidationResult.MemberNames.FirstOrDefault();
                    return;

                default:
                    Code = defaultCode;
                    Message = ex.Message;
                    Target = ex.Source;

                    var aggregateException = ex as AggregateException;
                    if (aggregateException != null)
                    {
                        Details = aggregateException.Flatten().InnerExceptions.Select(x => new ErrorModel(x));
                    }

                    return;
            }
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        [Required]
        public string Code { get; }

        /// <summary>
        /// Gets a human-readable representation of the error.
        /// </summary>
        [Required]
        public string Message { get; }

        /// <summary>
        /// Gets or sets the target to which this error applies.
        /// Might be a field in the case of a validation error, or the dependency name in the case of a dependency error.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets additional information about the error.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets additional details about the error.
        /// </summary>
        public IEnumerable<ErrorModel> Details { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"{Code} - {Message} ({string.Join(",", Details ?? Enumerable.Empty<ErrorModel>())})";
    }
}