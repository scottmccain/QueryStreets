using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerReader.Domain
{
    public class StreetSummary
    {
        public int TigerLineId { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
        public string FullAddress { get; set; }
    }
}
