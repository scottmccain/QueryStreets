﻿using System;
using System.Collections.Generic;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{
    public class RecordType5Converter : IClassConvert<RecordType5>
    {
        private readonly Func<Dictionary<string, string>, RecordType5> _converter;
        public RecordType5Converter()
        {
            _converter = ConversionFactory.GetRecordType5Conversion();
        }

        #region IClassConvert<RecordType5> Members

        public RecordType5 Convert(Dictionary<string, string> fromValues)
        {
            return _converter(fromValues);
        }

        #endregion
    }
}
