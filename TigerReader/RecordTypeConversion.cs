using System;
using System.Collections.Generic;
using TIGERConverters;

namespace TIGER_Reader
{
    public class RecordTypeConversion
    {
        public string Extension { get; set; }
        public Type DataType { get; set; }
        public TigerLineRecordType TigerLineRecordType { get; set; }
        public List<Dictionary<string, string>> Rows { get; set; }
        public Dictionary<string, string> this[int index]
        {
            get { return Rows[index]; }
        }
    }
}