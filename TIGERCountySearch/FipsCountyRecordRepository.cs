﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using TIGERShared;

namespace TIGERCountySearch
{
    public class FipsCountyRecordRepository
    {
        const string StateCodeKey = "State Code";
        const string CountyCodeKey = "County Code";
        const string CountyNameKey = "County Name";

        const int StateColumnStart = 0;
        const int CountyCodeColumnStart = 3;
        const int CountyNameColumnStart = 7;

        const int StateColumnLength = 3;
        const int CountyCodeColumnLength = 4;
        const int CountyNameColumnLength = 60;

        private readonly List<FipsCountyRecord> _fipsRecords;

        public FipsCountyRecordRepository()
        {
            _fipsRecords = ReadFipsCountyRecords();
        }

        public FipsCountyRecord FindRecordByCountyName(string countyName)
        {
            var query = from record in _fipsRecords
                        where record.CountyName.Contains(countyName)
                        select record;

            return query.FirstOrDefault();
        }

        private static List<FipsCountyRecord> ReadFipsCountyRecords()
        {
            var fipsDictionary = new List<FixedColumnDictionaryEntry>()
            {
                new FixedColumnDictionaryEntry() { Name = StateCodeKey, ColumnStart = StateColumnStart, Length = StateColumnLength },
                new FixedColumnDictionaryEntry(){ Name = CountyCodeKey, ColumnStart = CountyCodeColumnStart, Length = CountyCodeColumnLength },
                new FixedColumnDictionaryEntry() { Name = CountyNameKey, ColumnStart = CountyNameColumnStart, Length = CountyNameColumnLength}
            };

            // read county fips codes
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TIGERCountySearch.fips_counties.txt"))
            {
                var fipsCountyReader = new FixedColumnReader(fipsDictionary);
                var records = fipsCountyReader.Read(stream);

                var query = from record in records
                            select new FipsCountyRecord()
                            {
                                StateCode = record[StateCodeKey],
                                CountyCode = record[CountyCodeKey],
                                CountyName = record[CountyNameKey]
                            };

                return query.ToList();
            }
        }


    }
}
