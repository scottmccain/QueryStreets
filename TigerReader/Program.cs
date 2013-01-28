using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using TIGERConverters;
using TIGERConverters.RecordTypes;
using TIGERCountySearch;
using TIGERShared;
using TIGER_Reader.Utils;
using TigerReader.DataServices;

namespace TIGER_Reader
{
    class Program
    {
        private object _syncLock = new object();
        private double _percentageComplete ;

        static void Main(String [] args)
        {
            var p = new Program();
            p.Run(args);
        }

        public void Run(String [] args)
        {
            // TODO: Refactor out
            var countyCode = string.Empty;
            var stateCode = string.Empty;

            // TODO: User supplied
            const string countyName = "San Diego";

            // TODO: User supplied
            const string stateName = "CA";

            var countyRepository = new FipsCountyRecordRepository();

            Console.Write("Do you want to download TIGERLine data (Y/N)? ");
            var keyInfo = Console.ReadKey();

            // TODO: Move into downloader component
            var countyRecord = countyRepository.FindRecordByCountyName(countyName);
            if (countyRecord != null)
            {
                countyCode = countyRecord.CountyCode;
                stateCode = countyRecord.StateCode;
            }

            var filename = string.Format("TGR{0}{1}.ZIP", stateCode, countyCode);

            Console.Clear();

            if (keyInfo.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Downloading {0}...", filename);
                DownloadTigerLineData(filename);
            }

            Console.WriteLine("Processing {0}...", filename);

            Console.WriteLine("Processing places...");
            ProcessPlaces(stateCode, countyCode, stateName, countyName, filename);

            Console.WriteLine("Building Type 1 Records...");
            List<RecordType1> recordType1List = ConvertRecordType1List(filename);

            Console.WriteLine("Processing Type 1 Records...");
            ProcessRecordType1List(stateCode, countyCode, recordType1List);

            Console.WriteLine("Processing Type 2 Records...");
            ProcessRecordType2List(filename, recordType1List);

            Console.WriteLine("Processing Type 4&5 Records...");
            ProcessRecordType4AndType5List(filename, recordType1List);

            Console.WriteLine("Processing Type 6 Records...");
            ProcessRecordType6List(filename, recordType1List);
            
        }

        private static void ProcessPlaces(string stateCode, string countyCode, string stateName, string countyName, string filename)
        {
            RecordTypeConversion conversionWrapper
                = GetRecordConversionWrapper<RecordTypeC>(filename, "rtc", TigerLineRecordType.RecordTypeC);

            // filter out records based on no fips
            var rtcFilter = from rtc in conversionWrapper.Rows
                            where rtc.ContainsKey("FIPS") && !string.IsNullOrWhiteSpace(rtc["FIPS"])
                            group rtc by stateCode.Trim() + countyCode.Trim() + rtc["FIPS"].Trim() into grouped
                            select grouped.First();

            conversionWrapper.Rows = rtcFilter.ToList();
            var recordTypeCList = ConvertRecordList(conversionWrapper, new RecordTypeCConverter());

            using (var ds = new TigerLineDataService())
            {
                foreach (var rtc in recordTypeCList)
                {
                    ds.CreatePlace(
                        string.Format(
                            "{0}{1}{2}", 
                            stateCode.Trim(), 
                            countyCode.Trim(), 
                            rtc.FIPS.Trim())
                        .SafeConvert<int>(),
                        stateCode, 
                        countyCode, 
                        rtc.FIPS, 
                        stateName, countyName, rtc.NAME);
                }
            }
        }

        private static void ProcessRecordType1List(string stateCode, string countyCode, ICollection<RecordType1> records)
        {
            using (var ds = new TigerLineDataService())
            {
                var count = records.Count;
                var current = 0;

                foreach (var record in records)
                {
                    var placeBuilder = new StringBuilder();

                    if (record.PLACEL != record.PLACER)
                    {
                        if (!string.IsNullOrEmpty(record.PLACEL))
                            placeBuilder.AppendFormat("{0}{1}{2}", stateCode, countyCode, record.PLACEL);
                        if (!string.IsNullOrEmpty(record.PLACER))
                        {
                            if (placeBuilder.Length != 0) placeBuilder.Append(",");
                            placeBuilder.AppendFormat("{0}{1}{2}", stateCode, countyCode, record.PLACER);
                        }
                    }
                    else
                    {
                        placeBuilder.AppendFormat("{0}{1}{2}", stateCode, countyCode, record.PLACEL);
                    }

                    ds.CreateStreet(record.TLID, record.CFCC, record.FEDIRP, record.FENAME, record.FETYPE, placeBuilder.ToString(), record.FEDIRP );

                    if (!string.IsNullOrEmpty(record.FRADDR))
                    {
                        var fromAddress = record.FRADDR;
                        if (fromAddress.Contains("-"))
                        {
                            var range = fromAddress.Split('-');
                            fromAddress = range[0] + range[1];
                        }

                        var toAddress = record.TOADDR;
                        if (toAddress.Contains("-"))
                        {
                            var range = toAddress.Split('-');
                            toAddress = range[0] + range[1];
                        }

                        ds.CreateAddress(record.TLID, -1, fromAddress, toAddress);
                    }

                    if (!string.IsNullOrEmpty(record.FRADDL))
                    {
                        var fromAddress = record.FRADDL;
                        if (fromAddress.Contains("-"))
                        {
                            var range = fromAddress.Split('-');
                            fromAddress = range[0] + range[1];
                        }

                        var toAddress = record.TOADDL;
                        if (toAddress.Contains("-"))
                        {
                            var range = toAddress.Split('-');
                            toAddress = range[0] + range[1];
                        }

                        ds.CreateAddress(record.TLID, -2, fromAddress, toAddress);
                    }

                    // add minimum and maximum street ranges
                    ds.CreateStreetSegment(record.TLID, 0, record.FRLAT, record.FRLONG);
                    ds.CreateStreetSegment(record.TLID, 5000, record.TOLAT, record.TOLONG);

                    if (current++%100 != 0) continue;
                    Console.Write("{0:0.00}% Complete\r", ((double)current / count) * 100);
                }
            }
        }

        private static void ProcessRecordType2List(string filename, IEnumerable<RecordType1> recordType1List)
        {
            var conversionWrapper
                = GetRecordConversionWrapper<RecordType2>(filename, "rt2", TigerLineRecordType.RecordType2);

            var recordType2List = ConvertRecordList(conversionWrapper, new RecordType2Converter());

            var query = from rt1 in recordType1List
                        join rt2 in recordType2List on rt1.TLID equals rt2.TLID
                        select rt2;

            using (var ds = new TigerLineDataService())
            {
                foreach (var record in query)
                {
                    double latitude, longitude;

                    var count = 0;
                    do
                    {
                        latitude = record.Latitude[count];
                        longitude = record.Longitude[count];

                        if (Math.Abs(latitude - 0) > Double.Epsilon && Math.Abs(longitude - 0) > Double.Epsilon)
                        {
                            ds.CreateStreetSegment(record.TLID, string.Format("{0}{1}", record.RTSQ, count).SafeConvert<int>(), latitude, longitude);
                        }

                        count++;

                    } while (Math.Abs(latitude - 0) > Double.Epsilon && Math.Abs(longitude - 0) > Double.Epsilon &&
                             count < 10);
                }
            }
        }

        private static void ProcessRecordType4AndType5List(string filename,
                                                            IEnumerable<RecordType1> recordType1List)
        {
            var conversionWrapper
                = GetRecordConversionWrapper<RecordType4>(filename, "rt4", TigerLineRecordType.RecordType4);

            var recordType4List = ConvertRecordList(conversionWrapper, new RecordType4Converter());

            conversionWrapper
                = GetRecordConversionWrapper<RecordType5>(filename, "rt5", TigerLineRecordType.RecordType5);

            var recordType5List = ConvertRecordList(conversionWrapper, new RecordType5Converter());

            if (recordType1List == null) return;

            var query = from rt1 in recordType1List
                        join rt4 in recordType4List on rt1.TLID equals rt4.TLID
                        select new { rt1.TLID, rt4.FeatureIds };

            using (var ds = new TigerLineDataService())
            {
                var queryRows = query.ToList();
                var rows = ds.GetStreetNamesByIdList(queryRows.Select(i => i.TLID).ToList());

                foreach (var record in queryRows)
                {
                    var tlid = record.TLID;
                    var filteredRows = rows.Where(i => i.TigerLineId == tlid).ToList();

                    foreach (var featureId in record.FeatureIds)
                    {
                        // found the first blank feature id
                        if (featureId == 0) break;

                        var id = featureId;
                        var findFeatureQuery = from rt5 in recordType5List
                                               where rt5.FEAT == id
                                               select rt5;

                        var foundFeature = findFeatureQuery.FirstOrDefault();

                        // and copy matching feature data from rt5
                        if (foundFeature == null) continue;

                        foreach (var streetNameRecord in filteredRows)
                        {
                            ds.CreateStreet(
                                record.TLID, 
                                streetNameRecord.CensusFeatureClassCode, 
                                foundFeature.FEDIRP, 
                                foundFeature.FENAME, 
                                foundFeature.FETYPE, 
                                streetNameRecord.PlaceId.ToString(CultureInfo.InvariantCulture), 
                                foundFeature.FEDIRS);
                        }
                    }
                }
            }
        }

        private static void ProcessRecordType6List(string filename, IEnumerable<RecordType1> recordType1List)
        {
            var conversionWrapper
                = GetRecordConversionWrapper<RecordType6>(filename, "rt6", TigerLineRecordType.RecordType6);

            var recordType6List = ConvertRecordList(conversionWrapper, new RecordType6Converter());

            var query = from rt1 in recordType1List
                        join rt6 in recordType6List on rt1.TLID equals rt6.TLID
                        select rt6;

            using (var ds = new TigerLineDataService())
            {
                foreach (var record in query)
                {
                    if (!string.IsNullOrEmpty(record.FRADDR))
                    {
                        var fromAddress = record.FRADDR;
                        if (fromAddress.Contains("-"))
                        {
                            var range = fromAddress.Split('-');
                            fromAddress = range[0] + range[1];
                        }

                        var toAddress = record.TOADDR;
                        if (toAddress.Contains("-"))
                        {
                            var range = toAddress.Split('-');
                            toAddress = range[0] + range[1];
                        }

                        ds.CreateAddress(record.TLID, -1, fromAddress, toAddress);
                    }

                    if (!string.IsNullOrEmpty(record.FRADDL))
                    {
                        var fromAddress = record.FRADDL;
                        if (fromAddress.Contains("-"))
                        {
                            var range = fromAddress.Split('-');
                            fromAddress = range[0] + range[1];
                        }

                        var toAddress = record.TOADDL;
                        if (toAddress.Contains("-"))
                        {
                            var range = toAddress.Split('-');
                            toAddress = range[0] + range[1];
                        }

                        ds.CreateAddress(record.TLID, -2, fromAddress, toAddress);
                    }
                }
            }
        }

        private static List<RecordType1> ConvertRecordType1List(string filename)
        {
            const string streetFeatureCode = "A";
            const string recordType1Extension = "rt1";

            var conversionWrapper
                = GetRecordConversionWrapper<RecordTypeC>(filename, recordType1Extension, TigerLineRecordType.RecordType1);

            var filter = from rt1 in conversionWrapper.Rows
                         where rt1[StringConstants.CensusFeatureClassCode].Substring(0, 1).Equals(streetFeatureCode) &&
                         (!string.IsNullOrEmpty(rt1[StringConstants.PlaceLeft]) || !string.IsNullOrEmpty(rt1[StringConstants.PlaceRight])) &&
                         (!string.IsNullOrEmpty(rt1[StringConstants.StartAddressLeft]) || !string.IsNullOrEmpty(rt1[StringConstants.StartAddressRight]))
                         select rt1;

            conversionWrapper.Rows = filter.ToList();
            return ConvertRecordList(conversionWrapper, new RecordType1Converter());
        }

        private static List<T> ConvertRecordList<T>(RecordTypeConversion rtc,
            IClassConvert<T> converter)
        {
            return rtc.Rows.Select(converter.Convert).ToList();
        }

        private static RecordTypeConversion GetRecordConversionWrapper<T>(string filename, string extension, TigerLineRecordType recordType)
        {
            var conversion = new RecordTypeConversion
            {
                Extension = extension,
                DataType = typeof(T),
                TigerLineRecordType = recordType
            };

            var extractedFilename = ZipFileManager.ExtractFileByExtension(filename, @".\", extension);
            conversion.Rows = ProcessFile(extractedFilename, recordType);
            return conversion;
        }

        private static List<Dictionary<string, string>> ProcessFile(string filename, TigerLineRecordType recordType)
        {
            var repository = new RecordTypeDictionaryRepository();

            var recordTypeDictionary = repository.ReadRecordTypeDictionary(recordType);
            using (var tagfile = File.OpenRead(filename))
            {
                var reader = new FixedColumnReader(recordTypeDictionary);
                return reader.Read(tagfile);
            }
        }

        private static void DownloadTigerLineData(string zipFilename)
        {
            var client = new WebClient();
            client.DownloadFile(string.Format("http://www2.census.gov/geo/tiger/tiger2006se/CA/{0}", zipFilename), zipFilename);
        }

    }
}
