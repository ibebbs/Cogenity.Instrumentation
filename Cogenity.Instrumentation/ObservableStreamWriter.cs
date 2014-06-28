using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;

namespace Cogenity.Instrumentation
{
    public class ObservableStreamWriter : StreamWriter
    {
        private readonly IEventStreamSource _eventSource;
        private readonly Subject<string> _output;

        private IDisposable _disposable;

        private ObservableStreamWriter(ObservableStream stream) : base(stream, Encoding.UTF8, 1024)
        {
            _output = new Subject<string>();

            _disposable = new CompositeDisposable(
                stream.Output.Subscribe(_output),
                stream,
                _output
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

        public ObservableStreamWriter() : this(new ObservableStream()) { }

        public IObservable<string> Output
        {
            get { return _output; }
        }
    }
}
