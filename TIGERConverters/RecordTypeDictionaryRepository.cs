using System.Collections.Generic;
using System.Reflection;
using System.IO;
using TIGERShared;

namespace TIGERConverters
{
    public class RecordTypeDictionaryRepository
    {
        public static FixedColumnDictionary ReadRecordTypeDictionary(TigerLineRecordType type)
        {
            var dictionary = new FixedColumnDictionary();

            var recordTypeSuffixMap = new Dictionary<TigerLineRecordType, string>
                                          {
                { TigerLineRecordType.RecordType1, "rt1" },
                { TigerLineRecordType.RecordType2, "rt2" },
                { TigerLineRecordType.RecordType4, "rt4" },
                { TigerLineRecordType.RecordType5, "rt5" },
                { TigerLineRecordType.RecordType6, "rt6" },
                { TigerLineRecordType.RecordTypeC, "rtc" }
            };

            var dictionaryFilename = $"TIGERConverters.{recordTypeSuffixMap[type]}.dict";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dictionaryFilename))
            {
                if (stream == null) return dictionary;
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    do
                    {
                        line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var parts = line.Split(new[] { (char)9 });

                        var entry = new FixedColumnDictionaryEntry {Name = parts[0]};

                        int integerValue;
                        int.TryParse(parts[1], out integerValue);
                        entry.ColumnStart = integerValue - 1;

                        int.TryParse(parts[2], out integerValue);
                        entry.Length = integerValue;

                        dictionary.Add(entry);
                    } while (!string.IsNullOrEmpty(line));
                }
            }

            return dictionary;
        }

    }
}
