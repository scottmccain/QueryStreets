using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using TigerReader.DataServices;
using TigerReader.Domain;

namespace NewQueryStreets
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var program = new Program();
            program.Run(args);
        }

        public void Run(String[] args)
        {
            Console.Write("Enter Address: ");
            string addressPart = Console.ReadLine();

            Console.Write("Enter City: ");
            var cityName = Console.ReadLine();

            Console.Write("Enter State: ");
            var stateTerm = Console.ReadLine();

            Console.WriteLine("Searching...");

            GeoPoint foundPoint = null;

            var number = 0;

            if (addressPart != null)
            {
                var firstSpaceIndex = addressPart.IndexOf(' ');
                if (firstSpaceIndex != -1 && firstSpaceIndex + 1 < addressPart.Length)
                {
                    number = (int)Convert.ChangeType(addressPart.Substring(0, firstSpaceIndex), typeof(int));
                    addressPart = addressPart.Substring(firstSpaceIndex + 1);
                }
            }

            if( number == 0 ) throw new InvalidOperationException("Please specify a valid address");

            using (var ds = new TigerLineDataService())
            {
                var placeMatch = ds.GetPlaceByCityAndState(cityName, stateTerm);

                // was there an exact match?
                if (placeMatch == null)
                {
                    // if not, have to use levenshtein distance to find the closest match
                    var stateDistanceList = new List<dynamic>();


                    // now calculate the best levenshtein distance for city
                    foreach (var place in ds.GetPlacesByState(stateTerm))
                    {
                        var distance = Levenshtein.Compute(place.PlaceName, cityName);

                        dynamic distanceHolder = new ExpandoObject();
                        distanceHolder.Distance = distance;
                        distanceHolder.Record = place;

                        stateDistanceList.Add(distanceHolder);
                    }

                    var distanceQuery = from dno in stateDistanceList
                                        orderby dno.Distance
                                        select dno.Record as Place;

                    var sortedList = distanceQuery.ToList();

                    placeMatch = sortedList.FirstOrDefault();
                }


                if (placeMatch != null)
                {
                    var result = ds.GetStreetSummaryByAddress(number, placeMatch.RecordId, addressPart);

                    //// we found something
                    //var result =
                    //    context.USP_CRUD_STREET_NAMES_FIND_BY_ADDRESS(number, foundPlace.Id, addressPart).FirstOrDefault
                    //        ();
                    if (result != null)
                    {
                        // get address range
                        //var firstAddress = (int) Convert.ChangeType(result.First ?? "0", typeof (int));
                        //var lastAddress = (int) Convert.ChangeType(result.Last ?? "0", typeof (int));
                        var firstAddress = result.First;
                        var lastAddress = result.Last;

                        //var addressRange = Math.Abs(firstAddress - lastAddress);

                        var segments = ds.GetStreetSegmentsByTigerLineIdOrdered(result.TigerLineId);

                        // find all segmenst for this TLID, ordered by sequence
                        //var segmentQuery = from segment in context.StreetSegments
                        //                   where segment.TLID == result.TLID
                        //                   orderby segment.Sequence
                        //                   select segment;

                        //var segments = segmentQuery.ToList();

                        // calcuate segment lenghts
                        var segmentLengths = new List<double>();
                        var numSegments = segments.Count - 1;
                        for (var i = 0; i < numSegments; i++)
                        {
                            var point1 = new GeoPoint(segments[i].Latitude, segments[i].Longitude);
                            var point2 = new GeoPoint(segments[i + 1].Latitude, segments[i + 1].Longitude);
                            segmentLengths.Add(CalcLineLength(point1, point2));
                        }

                        var totalLength = segmentLengths.Sum();
                        if (Math.Abs(totalLength - 0) < Double.Epsilon)
                        {
                            // distances are too small, return start of street
                            return;
                        }

                        var addressPosition = Math.Abs(number - lastAddress);
                        var addressCount = Math.Abs(firstAddress - lastAddress);

                        // get distance as a ratio of address to addresscount taken in terms of totalLength
                        var distanceAlongLine = (addressPosition/(double) addressCount)*totalLength;

                        // 1. Calculate total distance of segments by calculating the deltas between points
                        // 2. Estimate travel distance based on ratio between total distance and difference number and first address
                        // 3. Find segment that contains our address by adding travel distances up until we meet or exceed the travel distance calcuated in 2
                        // 4. Once segment is found, estimate bottom address of segment based on travel distance
                        // 5. Estimate remaining distance between segment and travel distance
                        // 6. Add that distance to the point at the beggining segment, convert back to lat/long and you have address

                        // Figure out which segment our address is in, and where it is
                        double travelDistance = 0;
                        for (var i = 0; i < numSegments; i++)
                        {
                            var bottomAddress = (int) (firstAddress + (travelDistance/totalLength*addressCount));

                            travelDistance += segmentLengths[i];
                            if (travelDistance < distanceAlongLine) continue;

                            // We've found our segment, do the final computations
                            var topAddress = (int) (firstAddress + (travelDistance/totalLength*addressCount));

                            // Determine how far along this segment our address is
                            var addressesForThisSegment = Math.Abs(topAddress - bottomAddress);
                            var addressLocationScale = Math.Abs(number - (double) bottomAddress)/addressesForThisSegment;

                            var point1 = new GeoPoint(segments[i].Latitude, segments[i].Longitude);
                            var point2 = new GeoPoint(segments[i + 1].Latitude, segments[i + 1].Longitude);

                            var sqlPoint =
                                point1.CalculateDestinationPoint(
                                    point1.CalculateDistanceTo(point2)*addressLocationScale,
                                    point1.CalculateBearingTo(point2));
                            foundPoint = new GeoPoint(sqlPoint.Latitude, sqlPoint.Longitude);
                            break;
                        }
                    }
                }
            }

            if (foundPoint != null)
            {
                Console.WriteLine("Found point: {0}, {1}", foundPoint.Latitude, foundPoint.Longitude);
            }
            else
            {
                Console.WriteLine("Not found.");
            }

            Console.ReadKey();
            
        }

        private static double CalcLineLength(GeoPoint p1, GeoPoint p2)
        {
            var deltaX = Math.Abs(p1.Latitude - p2.Latitude);
            var deltaY = Math.Abs(p1.Longitude - p2.Longitude);

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }


}
