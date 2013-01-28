using System;
using System.Collections.Generic;
using TIGERConverters.RecordTypes;

namespace TIGERConverters
{
    public class ConversionFactory
    {
        public static Func<Dictionary<string, string>, RecordType6> GetRecordType6Conversion()
        {
            return record =>
                       {
                           var rt = new RecordType6
                                        {
                                            FRADDL = record[StringConstants.StartAddressLeft],
                                            FRADDR = record[StringConstants.StartAddressRight],
                                            RTSQ = record[StringConstants.RecordSequenceNumber].SafeConvert<int>(),
                                            TLID = record[StringConstants.TigerLineId].SafeConvert<int>(),
                                            TOADDL = record[StringConstants.EndAddressLeft],
                                            TOADDR = record[StringConstants.EndAddressRight]
                                        };

                           return rt;
                       };
        }

        public static Func<Dictionary<string, string>, RecordType5> GetRecordType5Conversion()
        {
            return record =>
                       {
                           var rt = new RecordType5
                                                {
                                                    FEAT = record[StringConstants.Feature].SafeConvert<int>(),
                                                    FEDIRP = record[StringConstants.FeatureDirectionPrefix],
                                                    FEDIRS = record[StringConstants.FeatureDirectionSuffix],
                                                    FENAME = record[StringConstants.FeatureName],
                                                    FETYPE = record[StringConstants.FeatureType]
                                                };

                           return rt;
                       };
        }

        public static Func<Dictionary<string, string>, RecordType4> GetRecordType4Conversion()
        {
            return record =>
                       {
                           var featureIds = new[] { 0, 0, 0, 0, 0 };

                           if( !string.IsNullOrWhiteSpace(record[StringConstants.Feature1]) )
                               featureIds[0] = record[StringConstants.Feature1].SafeConvert<int>();

                           if (!string.IsNullOrWhiteSpace(record[StringConstants.Feature2]))
                               featureIds[1] = record[StringConstants.Feature2].SafeConvert<int>();

                           if (!string.IsNullOrWhiteSpace(record[StringConstants.Feature3]))
                               featureIds[2] = record[StringConstants.Feature3].SafeConvert<int>();

                           if (!string.IsNullOrWhiteSpace(record[StringConstants.Feature4]))
                               featureIds[3] = record[StringConstants.Feature4].SafeConvert<int>();

                           if (!string.IsNullOrWhiteSpace(record[StringConstants.Feature5]))
                               featureIds[4] = record[StringConstants.Feature5].SafeConvert<int>();

                           var rt = new RecordType4
                                                {
                                                    TLID = record[StringConstants.TigerLineId].SafeConvert<int>(),
                                                    RTSQ = record[StringConstants.RecordSequenceNumber].SafeConvert<int>(),
                                                    FeatureIds = featureIds
                                                };

                           return rt;
                       };
        }


        public static Func<Dictionary<string, string>, RecordType2> GetRecordType2Conversion()
        {
            return record =>
                       {
                           var latitudes = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                           var longitudes = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                           latitudes[0] = (record[StringConstants.Point1Latitude].Substring(0, record[StringConstants.Point1Latitude].Length - 6) + "." + record[StringConstants.Point1Latitude].Substring(record[StringConstants.Point1Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[1] = (record[StringConstants.Point2Latitude].Substring(0, record[StringConstants.Point2Latitude].Length - 6) + "." + record[StringConstants.Point2Latitude].Substring(record[StringConstants.Point2Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[2] = (record[StringConstants.Point3Latitude].Substring(0, record[StringConstants.Point3Latitude].Length - 6) + "." + record[StringConstants.Point3Latitude].Substring(record[StringConstants.Point3Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[3] = (record[StringConstants.Point4Latitude].Substring(0, record[StringConstants.Point4Latitude].Length - 6) + "." + record[StringConstants.Point4Latitude].Substring(record[StringConstants.Point4Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[4] = (record[StringConstants.Point5Latitude].Substring(0, record[StringConstants.Point5Latitude].Length - 6) + "." + record[StringConstants.Point5Latitude].Substring(record[StringConstants.Point5Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[5] = (record[StringConstants.Point6Latitude].Substring(0, record[StringConstants.Point6Latitude].Length - 6) + "." + record[StringConstants.Point6Latitude].Substring(record[StringConstants.Point6Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[6] = (record[StringConstants.Point7Latitude].Substring(0, record[StringConstants.Point7Latitude].Length - 6) + "." + record[StringConstants.Point7Latitude].Substring(record[StringConstants.Point7Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[7] = (record[StringConstants.Point8Latitude].Substring(0, record[StringConstants.Point8Latitude].Length - 6) + "." + record[StringConstants.Point8Latitude].Substring(record[StringConstants.Point8Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[8] = (record[StringConstants.Point9Latitude].Substring(0, record[StringConstants.Point9Latitude].Length - 6) + "." + record[StringConstants.Point9Latitude].Substring(record[StringConstants.Point9Latitude].Length - 6, 6)).SafeConvert<double>();
                           latitudes[9] = (record[StringConstants.Point10Latitude].Substring(0, record[StringConstants.Point10Latitude].Length - 6) + "." + record[StringConstants.Point10Latitude].Substring(record[StringConstants.Point10Latitude].Length - 6, 6)).SafeConvert<double>();

                           longitudes[0] = (record[StringConstants.Point1Longitude].Substring(0, record[StringConstants.Point1Longitude].Length - 6) + "." + record[StringConstants.Point1Longitude].Substring(record[StringConstants.Point1Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[1] = (record[StringConstants.Point2Longitude].Substring(0, record[StringConstants.Point2Longitude].Length - 6) + "." + record[StringConstants.Point2Longitude].Substring(record[StringConstants.Point2Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[2] = (record[StringConstants.Point3Longitude].Substring(0, record[StringConstants.Point3Longitude].Length - 6) + "." + record[StringConstants.Point3Longitude].Substring(record[StringConstants.Point3Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[3] = (record[StringConstants.Point4Longitude].Substring(0, record[StringConstants.Point4Longitude].Length - 6) + "." + record[StringConstants.Point4Longitude].Substring(record[StringConstants.Point4Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[4] = (record[StringConstants.Point5Longitude].Substring(0, record[StringConstants.Point5Longitude].Length - 6) + "." + record[StringConstants.Point5Longitude].Substring(record[StringConstants.Point5Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[5] = (record[StringConstants.Point6Longitude].Substring(0, record[StringConstants.Point6Longitude].Length - 6) + "." + record[StringConstants.Point6Longitude].Substring(record[StringConstants.Point6Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[6] = (record[StringConstants.Point7Longitude].Substring(0, record[StringConstants.Point7Longitude].Length - 6) + "." + record[StringConstants.Point7Longitude].Substring(record[StringConstants.Point7Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[7] = (record[StringConstants.Point8Longitude].Substring(0, record[StringConstants.Point8Longitude].Length - 6) + "." + record[StringConstants.Point8Longitude].Substring(record[StringConstants.Point8Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[8] = (record[StringConstants.Point9Longitude].Substring(0, record[StringConstants.Point9Longitude].Length - 6) + "." + record[StringConstants.Point9Longitude].Substring(record[StringConstants.Point9Longitude].Length - 6, 6)).SafeConvert<double>();
                           longitudes[9] = (record[StringConstants.Point10Longitude].Substring(0, record[StringConstants.Point10Longitude].Length - 6) + "." + record[StringConstants.Point10Longitude].Substring(record[StringConstants.Point10Longitude].Length - 6, 6)).SafeConvert<double>();

                           var rt = new RecordType2
                                                {
                                                    TLID = record[StringConstants.TigerLineId].SafeConvert<int>(),
                                                    RTSQ = record[StringConstants.RecordSequenceNumber].SafeConvert<int>(),
                                                    Latitude = latitudes,
                                                    Longitude = longitudes
                                                };

                           return rt;
                       };
        }

        public static Func<Dictionary<string, string>, RecordTypeC> GetRecordTypeCConversion()
        {
            return record => new RecordTypeC
                                 {
                                       FIPS = record[StringConstants.FipsCode],
                                       FIPSTYPE = record[StringConstants.FipsType],
                                       NAME = record[StringConstants.Name]
                                   };
        }

        public static Func<Dictionary<string, string>, RecordType1> GetRecordType1Conversion()
        {
            return record => new RecordType1
                                 {
                                     CFCC = record[StringConstants.CensusFeatureClassCode],
                                     FRADDL = record[StringConstants.StartAddressLeft],
                                     FRADDR = record[StringConstants.StartAddressRight],
                                     PLACEL = record[StringConstants.PlaceLeft],
                                     PLACER = record[StringConstants.PlaceRight],
                                     FRLAT = (record[StringConstants.StartLatitude].Substring(0, record[StringConstants.StartLatitude].Length - 6) + "." + record[StringConstants.StartLatitude].Substring(record[StringConstants.StartLatitude].Length - 6, 6)).SafeConvert<double>(),
                                     FRLONG = (record[StringConstants.StartLongitude].Substring(0, record[StringConstants.StartLongitude].Length - 6) + "." + record[StringConstants.StartLongitude].Substring(record[StringConstants.StartLongitude].Length - 6, 6)).SafeConvert<double>(),
                                     TOLAT = (record[StringConstants.EndLatitude].Substring(0, record[StringConstants.EndLatitude].Length - 6) + "." + record[StringConstants.EndLatitude].Substring(record[StringConstants.EndLatitude].Length - 6, 6)).SafeConvert<double>(),
                                     TOLONG = (record[StringConstants.EndLongitude].Substring(0, record[StringConstants.EndLongitude].Length - 6) + "." + record[StringConstants.EndLongitude].Substring(record[StringConstants.EndLongitude].Length - 6, 6)).SafeConvert<double>(),
                                     TLID = record[StringConstants.TigerLineId].SafeConvert<int>(),
                                     FEDIRP = record[StringConstants.FeatureDirectionPrefix],
                                     FENAME = record[StringConstants.FeatureName],
                                     FETYPE = record[StringConstants.FeatureType],
                                     FEDIRS = record[StringConstants.FeatureDirectionSuffix],
                                     TOADDL = record[StringConstants.EndAddressLeft],
                                     TOADDR = record[StringConstants.EndAddressRight]
                                 };
        }

    }
}
