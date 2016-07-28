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
    public class RecordReader<T, TConvert> : IDisposable, IEnumerable<T> where TConvert : class, IClassConvert<T>, new()
    {
        private readonly FixedColumnStreamReader _reader;
        private readonly IClassConvert<T> _converter;

        public RecordReader(Stream stream)
        {
            _converter = new TConvert();
            _reader = new FixedColumnStreamReader(stream, RecordTypeDictionaryRepository.ReadRecordTypeDictionary(_converter.RecordType));
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
            _reader.Dispose();
        }
    }
}
