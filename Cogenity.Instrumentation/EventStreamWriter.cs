using Cogenity.IO;
using EventSourceProxy;
using System;
using System.IO;
using System.Reactive.Disposables;
using System.Text;

namespace Cogenity.Instrumentation
{
    public class EventStreamWriter : StreamWriter
    {
        private readonly IEventStreamSource _eventSource;

        private IDisposable _disposable;

        private EventStreamWriter(string name, ObservableStream stream) : base(stream, Encoding.UTF8, 1024)
        {
            _eventSource = EventSourceImplementer.GetEventSourceAs<IEventStreamSource>();

            _disposable = new CompositeDisposable(
                stream.Output.Subscribe(value => _eventSource.Write(name, value)),
                stream
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing & _disposable != null)
            {
                _disposable.Dispose();
                _disposable = null;
            }

            base.Dispose(disposing);
        }

        public EventStreamWriter(string name) : this(name, new ObservableStream()) { }
    }
}
