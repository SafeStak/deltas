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
        private static ErrorResponse DefaultErrorDetail => new ErrorResponse(
            HttpStatusCode.InternalServerError, ErrorCode.UnknownError, ErrorMessages.UnknownError);

        public ErrorResponse MapFromException(Exception ex)
        {
            return ex switch
            {
                PoolApiResponseException _ => new ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ErrorCode.PoolApiResponseException,
                    ErrorMessages.PoolApiException),
                _ => DefaultErrorDetail
            };
        }
    }
}
