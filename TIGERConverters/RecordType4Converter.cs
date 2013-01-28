using System;
using System.Collections.Generic;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{
    public class RecordType4Converter : IClassConvert<RecordType4>
    {
        private readonly Func<Dictionary<string, string>, RecordType4> _converter;

        public RecordType4Converter()
        {
            _converter = ConversionFactory.GetRecordType4Conversion();
        }

        #region IClassConvert<RecordType4> Members

        public RecordType4 Convert(Dictionary<string, string> fromValues)
        {
            return _converter(fromValues);
        }

        #endregion
    }
}
