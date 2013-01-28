using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerReader.Domain
{
    public class StreetName
    {
        public int RecordId { get; set; }
        public int TigerLineId { get; set; }
        public int PlaceId { get; set; }
        public string CensusFeatureClassCode { get; set; }
        public string DirectionPrefix { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DirectionSuffix { get; set; }
    }
}
