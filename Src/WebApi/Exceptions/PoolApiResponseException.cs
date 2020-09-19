using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public class PoolApiResponseException : ApplicationException, ILoggableException
    {
        public HttpResponseMessage Response { get; }

        public int EventId => 1001;

        public IDictionary<string, object> CustomProperties => throw new NotImplementedException();

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
