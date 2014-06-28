using EventSourceProxy;
using System.Diagnostics.Tracing;

namespace Cogenity.Instrumentation
{
    [EventSourceImplementation(Name = "EventStreamSource")]
    public interface IEventStreamSource
    {
        [Event(1, Message = "Write", Level = EventLevel.Informational)]
        void Write(string name, string text);
    }
}
