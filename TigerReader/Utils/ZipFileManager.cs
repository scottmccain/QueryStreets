using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace TIGER_Reader.Utils
{
    class ZipFileManager
    {
        public static string ExtractFileByExtension(string zipFilename, string outputPath, string extension)
        {
            string outputFilename = string.Empty;

            using (var zipStream = new ZipInputStream(File.OpenRead(zipFilename)))
            {
                ZipEntry zipEntry;
                while ((zipEntry = zipStream.GetNextEntry()) != null)
                {
                    //string extension = Path.GetExtension(zipEntry.Name);

                    var s = Path.GetExtension(zipEntry.Name);
                    if (s != null && (zipEntry.Name.EndsWith("/") || !s.Substring(1).Equals(extension,StringComparison.OrdinalIgnoreCase)))
                        continue;

                    outputFilename = zipEntry.Name.Replace("/", @"\");
                    outputFilename = Path.Combine(outputPath, outputFilename);

                    using (var streamWriter = File.Create(outputFilename))
                    {
                        var data = new byte[zipEntry.Size];

                        long bytesRead;
                        do
                        {
                            bytesRead = zipStream.Read(data, 0, data.Length);

                            if (bytesRead > 0)
                            {
                                streamWriter.Write(data, 0, (int)bytesRead);
                            }

                        } while (bytesRead > 0);
                    }

                    break;
                }
            }

            return outputFilename;
        }
    }
}
