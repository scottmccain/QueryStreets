using System.Collections.Generic;

namespace TIGERConverters
{
    public interface IClassConvert<out T>
    {
        T Convert(Dictionary<string, string> fromValues);
    }
}
