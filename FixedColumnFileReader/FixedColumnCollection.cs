using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FixedColumnFileCollection;

namespace FixedColumnFileCollection
{
    public class FixedColumnCollection<T> : IEnumerable<T>
    {
        private readonly FixedColumnStreamReader _reader;
        private readonly Func<Dictionary<string, string>, T> _converter;

        public FixedColumnCollection(Stream stream, FixedColumnDictionary columnDictionary, Func<Dictionary<string, string>, T> converter)
        {
            _converter = converter;
            _reader = new FixedColumnStreamReader(stream, columnDictionary);
        }

        public IEnumerator<T> GetEnumerator()
        {
            while(true)
            {
                var next = _reader.Next();

                if (next == null)
                    yield break;

                yield return _converter.Invoke(next);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
