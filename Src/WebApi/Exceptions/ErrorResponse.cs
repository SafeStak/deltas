using System.Net;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public static class ErrorMessages
    {
        public const string UnknownError = "An unknown error has occured.";
        public const string PoolApiException = "Unsuccessful response from the Pool API";
    }

    public class ErrorResponse
    {
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

        public string ErrorMessage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorCode { get; set; }
    }
}
