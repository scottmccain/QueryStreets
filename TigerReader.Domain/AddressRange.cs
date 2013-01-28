using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerReader.Domain
{
    public class AddressRange
    {
        public int RecordId { get; set; }
        public int TigerLineId { get; set; }
        public int RangeId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}
