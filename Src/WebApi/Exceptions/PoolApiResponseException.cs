using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public class PoolApiResponseException : ApplicationException, ILoggableException
    {
        public HttpRequestMessage Request { get; }

        public HttpResponseMessage Response { get; }

        public int EventId => 1001;

        public IDictionary<string, object> CustomProperties => ImmutableDictionary<string, object>.Empty;

        public PoolApiResponseException(string message, HttpRequestMessage request, HttpResponseMessage response) : this(message, request)
        {
            Response = response;
        }

        public PoolApiResponseException(string message, HttpRequestMessage request) : base(message)
        {
            Request = request;
        }

        public PoolApiResponseException(string message, HttpRequestMessage request, Exception ex) : base(message, ex)
        {
            Request = request;
        }
    }
}
