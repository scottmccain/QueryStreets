using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TIGERConverters;
using TIGERShared;

namespace TIGER_Reader
{
    // TODO: use reflection and/or injection to find TConvert
    public class RecordReader<T, TConvert> : IEnumerable<T>, IDisposable where TConvert : class, IClassConvert<T>, new()
    {
        private FixedColumnStreamReader _reader;
        private IClassConvert<T> _converter;
        private bool _disposed;

        public RecordReader(Stream stream)
        {
            _converter = new TConvert();
            _reader = new FixedColumnStreamReader(stream, RecordTypeDictionaryRepository.ReadRecordTypeDictionary(_converter.RecordType));
        }

        public void Close()
        {
            try
            {
                _reader.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Reset()
        {
            _reader.Rewind();
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!_reader.EndOfStream)
            {
                var next = _reader.Next();

                if(next == null)
                    yield break;
                
                yield return _converter.Convert(next);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            _reader.Dispose();
            _disposed = true;
        }
    }
}
