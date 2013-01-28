using System;

namespace TIGER_Reader
{
    static class StringExtension
    {
        public static T SafeConvert<T>(this string value) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
