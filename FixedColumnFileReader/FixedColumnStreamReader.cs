using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FixedColumnFileCollection
{
    public class FixedColumnStreamReader
    {
        private readonly StringReader _reader;
        private readonly FixedColumnDictionary _dictionary;
        private readonly string _buffer;

        public FixedColumnStreamReader(Stream stream, FixedColumnDictionary dictionary)
        {
            var textReader = new StreamReader(stream);
            _buffer = textReader.ReadToEnd();

            _dictionary = dictionary;
            _reader = new StringReader(_buffer);
        }

        //public bool EndOfStream => _reader.;

        public Dictionary<string, string> Next()
        {
            string line;

            do
            {
                line = _reader.ReadLine()?.Trim();
            } while (line != null && line.Length == 0);

            //while (!string.IsNullOrEmpty(line))
            //{
            //    line = _reader.ReadLine()?.Trim();
            //}

            if (string.IsNullOrEmpty(line)) return null;

            var values = new Dictionary<string, string>();
            foreach (var t in _dictionary.GetOrderdColumns())
            {
                var columnName = t.Name;
                var columnData = line?.Substring(t.ColumnStart, t.ColumnStart + t.Length > line.Length ? line.Length - t.ColumnStart : t.Length);

                values.Add(columnName, columnData?.Trim());
            }

            return values;
        }
    }
}
