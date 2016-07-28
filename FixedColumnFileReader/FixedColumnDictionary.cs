using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixedColumnFileCollection
{
    public class FixedColumnDictionary
    {
        private readonly List<IFixedColumnDictionaryEntry> _columns;

        public FixedColumnDictionary()
        {
            _columns = new List<IFixedColumnDictionaryEntry>();
        }

        public IEnumerable<IFixedColumnDictionaryEntry> GetOrderdColumns()
        {
            var query = from t in _columns
                        orderby t.ColumnStart
                        select t;

            return query.ToList();
        }


        public void Add(IFixedColumnDictionaryEntry entry)
        {
            _columns.Add(entry);
        }
    }
}
