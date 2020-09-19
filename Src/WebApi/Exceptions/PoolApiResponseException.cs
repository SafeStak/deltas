using System;
using System.Net.Http;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public class PoolApiResponseException : ApplicationException
    {
        public HttpResponseMessage Response { get; }

        public PoolApiResponseException(string message, HttpResponseMessage response) : base(message)
        {
            Response = response;
        }

        public PoolApiResponseException(string message, Exception ex) : base(message, ex)
        { }

        public PoolApiResponseException(string message) : base(message)
        { }
    }
}
