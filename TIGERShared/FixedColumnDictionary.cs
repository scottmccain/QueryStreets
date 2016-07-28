using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIGERShared
{
    public class FixedColumnDictionary
    {
        private readonly List<FixedColumnDictionaryEntry> _columns;

        public FixedColumnDictionary()
        {
            _columns = new List<FixedColumnDictionaryEntry>();
        }

        public IEnumerable<FixedColumnDictionaryEntry> GetOrderdColumns()
        {
            var query = from t in _columns
                        orderby t.ColumnStart
                        select t;

            return query.ToList();
        }


        public void Add(FixedColumnDictionaryEntry entry)
        {
            _columns.Add(entry);
        }
    }
}
