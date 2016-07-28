using System;
using System.Collections.Generic;

namespace FixedColumnFileCollection
{
    public interface IClassConvert<out T>
    {
        T Convert(Dictionary<string, string> fromValues);
        //TigerLineRecordType RecordType { get; }
    }
}
