using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerReader.Domain
{
    public class Place
    {
        public int RecordId { get; set; }
        public string StateCode { get; set; }
        public string CountyCode { get; set; }
        public string PlaceCode { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string PlaceName { get; set; }
    }
}
