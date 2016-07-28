﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FixedColumnFileCollection;

namespace TIGERShared
{
    public class FixedColumnStreamReader : IDisposable
    {
        private readonly StreamReader _reader;
        private readonly FixedColumnDictionary _dictionary;

        public FixedColumnStreamReader(Stream stream, FixedColumnDictionary dictionary)
        {
            _dictionary = dictionary;
            _reader = new StreamReader(stream);

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
            _reader.Dispose();
        }
    }
}
