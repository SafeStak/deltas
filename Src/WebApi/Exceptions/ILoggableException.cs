using System.Collections.Generic;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public interface ILoggableException
    {
        int EventId { get; }
        IDictionary<string, object> CustomProperties { get; }
    }
}
