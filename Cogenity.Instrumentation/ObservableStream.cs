using System;
using System.IO;
using System.Reactive.Subjects;
using System.Text;

namespace Cogenity.Instrumentation
{
    public class ObservableStream : Stream
    {
        private Subject<string> _output = new Subject<string>();

        public override void Write(byte[] buffer, int offset, int count)
        {
            _output.OnNext(Encoding.UTF8.GetString(buffer, offset, count));
        }

        public override bool CanRead { get { return false; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return true; } }
        public override void Flush() { }
        public override long Length { get { throw new InvalidOperationException(); } }
        public override int Read(byte[] buffer, int offset, int count) { throw new InvalidOperationException(); }
        public override long Seek(long offset, SeekOrigin origin) { throw new InvalidOperationException(); }
        public override void SetLength(long value) { throw new InvalidOperationException(); }
        public override long Position
        {
            get { throw new InvalidOperationException(); }
            set { throw new InvalidOperationException(); }
        }

        public IObservable<string> Output
        {
            get { return _output; }
        }
    }
}
