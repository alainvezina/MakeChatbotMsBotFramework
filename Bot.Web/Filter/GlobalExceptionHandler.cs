using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Bot.Web.Models.Errors;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Bot.Web.Filter
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly bool _includeStackTrace;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
        /// </summary>
        /// <param name="telemetryClient">The <see cref="TelemetryClient"/></param>
        /// <param name="includeStackTrace">True to include complete stack trace.</param>
        public GlobalExceptionHandler(TelemetryClient telemetryClient, bool includeStackTrace)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
            _includeStackTrace = includeStackTrace;
        }

        /// <inheritdoc/>
        public override void Handle(ExceptionHandlerContext context)
        {
            _telemetryClient.TrackException(new ExceptionTelemetry(context.Exception));

            var errorResponse = new ErrorResponseModel(context.Exception);
            if (_includeStackTrace)
            {
                errorResponse.Error.Data = context.Exception.StackTrace;
            }

            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var response = context.Request.CreateResponse(GetStatusCode(errorResponse), errorResponse, formatter);
            context.Result = new ResponseMessageResult(response);
        }

        private HttpStatusCode GetStatusCode(ErrorResponseModel errorResponse)
        {
            switch (errorResponse.Error.Code)
            {
                case ErrorCode.NotFoundError:
                    return HttpStatusCode.NotFound;
                case ErrorCode.ValidationError:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}