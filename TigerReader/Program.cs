using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;
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

        private static void Main(string[] args)
        {
            Task.Run(() =>
            {
                try
                {
                    Execute();
                }
                catch (Exception ex)
                {
                    // ignored
                }
            });

            Console.ReadLine();
        }

        private static void Execute()
        {
            var countyCode = string.Empty;
            var stateCode = string.Empty;

            const string countyName = "San Diego";
            const string stateName = "CA";

            //var countyRepository = new FipsCountyRecordRepository();

            //Console.Write("Do you want to download TIGERLine data (Y/N)? ");
            //var keyInfo = Console.ReadKey();

            //// TODO: Move into downloader component
            //var countyInfo = countyRepository.GetCountyInfo(countyName);
            //countyCode = countyInfo?.Item1;
            //stateCode = countyInfo?.Item2;

            //var filename = $"TGR{stateCode}{countyCode}.ZIP";

            //Console.Clear();
            //if (keyInfo.Key == ConsoleKey.Y)
            //{
            //    Console.WriteLine($"Downloading {filename}...");
            //    DownloadTigerLineData(filename);
            //}


            var path = $@"countyfiles\{stateName}\{countyName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            var filename = TigerDownload.GetCountyFile(countyName, path);
            Console.WriteLine($"Processing {countyName} ...");

            Console.WriteLine("Processing places...");
            ProcessPlaces(stateName, countyName, filename);

            Console.WriteLine("Building Type 1 Records...");
            var recordType1List = ConvertRecordType1List(filename);

            Console.WriteLine("Processing Type 1 Records...");
            ProcessRecordType1List(stateCode, countyCode, recordType1List);

            Console.WriteLine("Processing Type 2 Records...");
            ProcessRecordType2List(filename, recordType1List);

            Console.WriteLine("Processing Type 4&5 Records...");
            ProcessRecordType4AndType5List(filename, recordType1List);

            Console.WriteLine("Processing Type 6 Records...");
            ProcessRecordType6List(filename, recordType1List);
            
        }

        private static void ProcessPlaces(string countyName, string stateName, string zipfileLocation)
        {
            var countyInfo = FipsCountyRecordRepository.GetCountyInfo(countyName);
            var stateCode = countyInfo?.Item2?.Trim();
            var countyCode = countyInfo?.Item1?.Trim();

            // TODO: discover extension based on type
            var extractedFilename = ZipFileManager.ExtractFileByExtension(zipfileLocation, @".\", "rtc");

            // TODO: Figure out record converter based on type
            // use some reflection
            using (
                var reader = new RecordReader<RecordTypeC, RecordTypeCConverter>(File.OpenRead(extractedFilename)))
            {
                using (var ds = new TigerLineDataService())
                {
                    foreach (
                        var record in
                            reader.Where(r => !string.IsNullOrEmpty(r?.FIPS))
                                .GroupBy(r => stateCode + countyCode + r?.FIPS)
                                .Select(group => group?.First()))
                    {
                        ds.CreatePlace($"{stateCode}{countyCode}{record?.FIPS}".SafeConvert<int>(),
                            stateCode, countyCode, record?.FIPS, stateName, countyName, record?.NAME);
                    }
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
                            placeBuilder.Append($"{stateCode}{countyCode}{record.PLACEL}");
                        if (!string.IsNullOrEmpty(record.PLACER))
                        {
                            if (placeBuilder.Length != 0) placeBuilder.Append(",");
                            placeBuilder.Append($"{stateCode}{countyCode}{record.PLACER}");
                        }
                    }
                    else
                    {
                        placeBuilder.Append($"{stateCode}{countyCode}{record.PLACEL}");
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
                    Console.Write($"{((double) current/count)*100:0.00}% Complete\r");
                }
            }
        }

        private static void ProcessRecordType2List(string filename, IEnumerable<RecordType1> recordType1List)
        {
            var conversionWrapper
                = GetRecordConversionWrapper(filename, "rt2", TigerLineRecordType.RecordType2);

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

                        if (Math.Abs(latitude - 0) > double.Epsilon && Math.Abs(longitude - 0) > double.Epsilon)
                        {
                            ds.CreateStreetSegment(record.TLID, $"{record.RTSQ}{count}".SafeConvert<int>(), latitude, longitude);
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
                = GetRecordConversionWrapper(filename, "rt4", TigerLineRecordType.RecordType4);

            var recordType4List = ConvertRecordList(conversionWrapper, new RecordType4Converter());

            conversionWrapper
                = GetRecordConversionWrapper(filename, "rt5", TigerLineRecordType.RecordType5);

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
                = GetRecordConversionWrapper(filename, "rt6", TigerLineRecordType.RecordType6);

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
                = GetRecordConversionWrapper(filename, recordType1Extension, TigerLineRecordType.RecordType1);

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

        private static IEnumerable<T> ConvertRecordList<T>(IEnumerable<Dictionary<string, string>> rows, IClassConvert<T> converter)
        {
            return rows.Select(converter.Convert);
        } 
            

        private static RecordTypeConversion GetRecordConversionWrapper(string filename, string extension, TigerLineRecordType recordType)
        {
            var conversion = new RecordTypeConversion();

            var extractedFilename = ZipFileManager.ExtractFileByExtension(filename, @".\", extension);
            //conversion.Rows = ProcessFile(extractedFilename, recordType);
            return conversion;
        }


        //private static IEnumerable<T> ProcessFile<T>(string filename, TigerLineRecordType recordType, I)
        //{
        //    return new RecordReader(File.OpenRead(filename), recordType);
        //    //var repository = new RecordTypeDictionaryRepository();

        //    //var recordTypeDictionary = RecordTypeDictionaryRepository.ReadRecordTypeDictionary(recordType);
        //    //using (var tagfile = File.OpenRead(filename))
        //    //{
        //    //var reader = new FixedColumnReader(recordTypeDictionary);
        //    //
        //    //return reader.Read(tagfile);


        //    //var list = new List<Dictionary<string, string>>();
        //    using (var reader = new RecordReader(File.OpenRead(filename), recordType))
        //    {
        //        return reader.ToList();
        //    }
        //    //}

        //    //return list;
        //}


        private static IEnumerable<Dictionary<string, string>> ProcessFile(string filename, TigerLineRecordType recordType)
        {
            //return new RecordReader(File.OpenRead(filename), recordType);
            //var repository = new RecordTypeDictionaryRepository();

            //var recordTypeDictionary = RecordTypeDictionaryRepository.ReadRecordTypeDictionary(recordType);
            //using (var tagfile = File.OpenRead(filename))
            //{
            //var reader = new FixedColumnReader(recordTypeDictionary);
            //
            //return reader.Read(tagfile);


            //var list = new List<Dictionary<string, string>>();
            //using (var reader = new RecordReader(File.OpenRead(filename), recordType))
            //{
            //    return reader.ToList();
            //}
            //}

            //return list;

            return new Dictionary<string, string>[0];
        }

        private static void DownloadTigerLineData(string zipFilename)
        {
            var client = new WebClient();
            client.DownloadFile($"http://www2.census.gov/geo/tiger/tiger2006se/CA/{zipFilename}", zipFilename);
        }

    }
}
