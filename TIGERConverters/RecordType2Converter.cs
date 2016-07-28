using System;
using System.Collections.Generic;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{
    public class RecordType2Converter : IClassConvert<RecordType2>
    {
        private readonly Func<Dictionary<string, string>, RecordType2> _convert;

        public RecordType2Converter()
        {
            _convert = ConversionFactory.GetRecordType2Conversion();
        }

        #region IClassConvert<RecordType2> Members

        public RecordType2 Convert(Dictionary<string, string> fromValues)
        {
            return _convert(fromValues);
        }

        public TigerLineRecordType RecordType => TigerLineRecordType.RecordType2;

        #endregion
    }
}
