using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TIGERShared
{
    public class FixedColumnStreamReader : IDisposable
    {
        private readonly Stream _stream;
        private readonly StreamReader _reader;
        private readonly FixedColumnDictionary _dictionary;
        private bool _disposed;

        public FixedColumnStreamReader(Stream stream, FixedColumnDictionary dictionary)
        {
            _stream = stream;
            _dictionary = dictionary;
            _reader = new StreamReader(_stream);

        }

        public void Close()
        {
            _stream.Close();
        }

        public void Rewind()
        {
            _stream.Seek(0, SeekOrigin.Begin);
        }


        public bool EndOfStream => _reader.EndOfStream;

        public Dictionary<string, string> Next()
        {
            var line = _reader.ReadLine()?.Trim();

            while (!_reader.EndOfStream && string.IsNullOrEmpty(line))
            {
                line = _reader.ReadLine()?.Trim();
            }

            if (_reader.EndOfStream) return null;

            var values = new Dictionary<string, string>();
            foreach (var t in _dictionary.GetOrderdColumns())
            {
                var columnName = t.Name;
                var columnData = line?.Substring(t.ColumnStart, t.ColumnStart + t.Length > line.Length ? line.Length - t.ColumnStart : t.Length);

                values.Add(columnName, columnData?.Trim());
            }

            return values;
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

            // Free any unmanaged objects here.
            //
            try
            {
                _stream.Close();
                _stream.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }

            _disposed = true;
        }
    }
}
