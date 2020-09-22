using System;
using System.Net;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public interface IErrorResponseMapper
    {
        ErrorResponse MapFromException(Exception ex);
    }

    public class ErrorResponseMapper : IErrorResponseMapper
    {
        public const string UnknownError = "Oops! An unknown error has occured. Please try again later";
        public const string PoolApiException = "Uh oh! We received an unsuccessful response from the Pool API";

        private static ErrorResponse DefaultErrorDetail => new ErrorResponse(
            HttpStatusCode.InternalServerError, ErrorCode.UnknownError, UnknownError);

        public ErrorResponse MapFromException(Exception ex)
        {
            return ex switch
            {
                PoolApiResponseException _ => new ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ErrorCode.PoolApiResponseException,
                    PoolApiException),
                _ => DefaultErrorDetail
            };
        }
    }
}
