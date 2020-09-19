using System.Net;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public enum ErrorCode
    {
        UnknownError,
        InvalidRequest,
        PoolApiResponseException
    }

    public class ErrorResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorResponse(HttpStatusCode httpStatusCode, ErrorCode errorCode, string errorMessage)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode.ToString();
            ErrorMessage = errorMessage;
        }

        public ErrorResponse(HttpStatusCode httpStatusCode, string errorCode, string errorMessage)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
