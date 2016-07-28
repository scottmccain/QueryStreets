﻿using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FixedColumnFileCollection
{
    public class FixedColumnReader
    {
        private readonly List<IFixedColumnDictionaryEntry> _dictionary;

        public FixedColumnReader(List<IFixedColumnDictionaryEntry> dictionary)
        {
            _dictionary = dictionary;
        }

        private List<IFixedColumnDictionaryEntry> GetOrderedFixedColumnDictionary()
        {
            var query = from t in _dictionary
                        orderby t.ColumnStart
                        select t;

            return query.ToList();
        }

        private IEnumerable<Dictionary<string, string>> Read(string data)
        {
            var records = new List<Dictionary<string, string>>();

            var reader = new StringReader(data);

            var orderedDictionary = GetOrderedFixedColumnDictionary();

            string line;
            do
            {
                line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var values = new Dictionary<string, string>();

                foreach (var t in orderedDictionary)
                {
                    var columnName = t.Name;
                    var columnData = line.Substring(t.ColumnStart, t.ColumnStart + t.Length > line.Length ? line.Length - t.ColumnStart : t.Length);

                    values.Add(columnName, columnData.Trim());
                }

                records.Add(values);
            } while (!string.IsNullOrEmpty(line));


            return records;
        }

        public IEnumerable<Dictionary<string, string>> Read(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Read(reader.ReadToEnd());
            }
        }
    }
}
