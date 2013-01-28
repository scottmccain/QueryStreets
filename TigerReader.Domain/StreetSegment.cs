using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerReader.Domain
{
    public class StreetSegment
    {
        public int RecordId { get; set; }
        public int TigerLineId { get; set; }
        public int Sequence { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
