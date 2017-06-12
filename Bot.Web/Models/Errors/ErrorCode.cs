using System.Web;

namespace Bot.Web.Models.Errors
{
    /// <summary>
    /// Standard error codes.
    /// </summary>
    public static class ErrorCode
    {
        /// <summary>
        /// Internal server error.
        /// </summary>
        public const string InternalServerError = "internalServerError";

        /// <summary>
        /// A functional validation error.
        /// </summary>
        public const string ValidationError = "validationError";

        /// <summary>
        /// Indicates that the item could not be found.
        /// </summary>
        public const string NotFoundError = "notFoundError";
    }
}