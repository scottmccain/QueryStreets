using System;
using System.Collections.Generic;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{
    public class RecordType6Converter : IClassConvert<RecordType6>
    {
        private readonly Func<Dictionary<string, string>, RecordType6> _converter;
             
        public RecordType6Converter()
        {
            _converter = ConversionFactory.GetRecordType6Conversion();
        }

        #region IClassConvert<RecordType6> Members

        public RecordType6 Convert(Dictionary<string, string> fromValues)
        {
            return _converter(fromValues);
        }

        public TigerLineRecordType RecordType => TigerLineRecordType.RecordType6;

        #endregion
    }
}
