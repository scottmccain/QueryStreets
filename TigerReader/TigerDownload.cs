using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TIGERCountySearch;

namespace TIGERShared
{
    public class TigerDownload
    {
        public static string GetCountyFile(string countyName, string path)
        {
            var countyInfo = FipsCountyRecordRepository.GetCountyInfo(countyName);
            var filename = $"TGR{countyInfo?.Item2}{countyInfo?.Item1}.ZIP";
            var zipFilepath = Path.Combine(path, filename);

            if (File.Exists(zipFilepath)) return zipFilepath;

            var client = new WebClient();
            client.DownloadFile($"http://www2.census.gov/geo/tiger/tiger2006se/CA/{filename}", zipFilepath);

            return zipFilepath;
        }
    }
}
