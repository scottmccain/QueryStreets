using System;
using System.Collections.Generic;
using FixedColumnFileCollection;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{

    public class RecordType1Converter : IClassConvert<RecordType1>
    {
        private readonly Func<Dictionary<string, string>, RecordType1> _convert;

        public RecordType1Converter()
        {
            _convert = ConversionFactory.GetRecordType1Conversion();
        }


        #region IClassConvert<T> Members

        public RecordType1 Convert(Dictionary<string, string> fromValues)
        {
            return _convert(fromValues);
        }

        public TigerLineRecordType RecordType => TigerLineRecordType.RecordType1;

        #endregion
    }
}
