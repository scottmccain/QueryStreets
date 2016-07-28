using System.Runtime.Serialization;

namespace TIGERConverters.RecordTypes
{
    // ReSharper disable InconsistentNaming
    // todo: custom attribute for each data member
    // attribute called fixedwidthcolumn, properties are Name (same as DataMember), start and length - only start and length are required
    public class RecordTypeC
    {
        [DataMember(Name = "FIPS")]
        public string Fips { get; set; }

        [DataMember(Name = "FIPSTYPE")]
        public string FipsType { get; set; }

        [DataMember(Name = "NAME")]
        public string Name { get; set; }
    }
    // ReSharper restore InconsistentNaming
}
