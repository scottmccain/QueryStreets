using System;
using System.Collections.Generic;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{
    public class RecordTypeCConverter : IClassConvert<RecordTypeC>
    {
        private readonly Func<Dictionary<string, string>, RecordTypeC> _convert;

        public RecordTypeCConverter()
        {
            _convert = ConversionFactory.GetRecordTypeCConversion();
        }

        #region IClassConvert<RecordTypeC> Members

        public RecordTypeC Convert(Dictionary<string, string> fromValues)
        {
            return _convert(fromValues);
        }

        #endregion
    }
}
