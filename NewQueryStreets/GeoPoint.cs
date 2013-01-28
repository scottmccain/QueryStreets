using System;

namespace NewQueryStreets
{
    class GeoPoint
    {
        const double radius = 6371; // earths radius
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoPoint"/> class.
        /// </summary>
        /// <param name="lat">The lat.</param>
        /// <param name="longitude">The longitude.</param>
        public GeoPoint(double lat, double longitude)
        {
            Latitude = lat;
            Longitude = longitude;
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Calculates the distance to.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public double CalculateDistanceTo(GeoPoint point)
        {
            double R = radius;
            double lat1 = Latitude * Math.PI / 180, lon1 = Longitude * Math.PI / 180;
            double lat2 = point.Latitude * Math.PI / 180, lon2 = point.Longitude * Math.PI / 180;
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) +
                Math.Cos(lat1) * Math.Cos(lat2) * 
                Math.Sin(dLon/2) * Math.Sin(dLon/2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double d = R * c;
            
            return d;        
        }

        /// <summary>
        /// Calculates the destination point.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="bearing">The bearing.</param>
        /// <returns></returns>
        public GeoPoint CalculateDestinationPoint(double distance, double bearing)
        {
            distance = distance/radius;  // convert dist to angular distance in radians
            bearing = bearing * Math.PI / 180;  // 
            double lat1 = Latitude * Math.PI / 180, lon1 = Longitude * Math.PI / 180;

            double lat2 = Math.Asin( Math.Sin(lat1)*Math.Cos(distance) + 
                        Math.Cos(lat1)*Math.Sin(distance)*Math.Cos(bearing) );

            double lon2 = lon1 + Math.Atan2(Math.Sin(bearing)*Math.Sin(distance)*Math.Cos(lat1), 
                               Math.Cos(distance)-Math.Sin(lat1)*Math.Sin(lat2));

            lon2 = (lon2+3*Math.PI)%(2*Math.PI) - Math.PI;  // normalise to -180...+180

            return new GeoPoint(lat2 * 180 / Math.PI, lon2 * 180 / Math.PI);
        }

        /// <summary>
        /// Calculates the bearing to.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public double CalculateBearingTo(GeoPoint point)
        {
            double lat1 = Latitude * Math.PI / 180, lat2 = point.Latitude * Math.PI / 180;
            double dLon = (point.Longitude - Longitude) * Math.PI / 180;

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) -
                    Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            
            var brng = Math.Atan2(y, x);

            return (brng * 180 / Math.PI + 360) % 360;
        }

        /// <summary>
        /// Calculates the intersection.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="brng1">The BRNG1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="brng2">The BRNG2.</param>
        /// <returns></returns>
        public GeoPoint CalculateIntersection(GeoPoint p1, double brng1, GeoPoint p2, double brng2)
        {
            double lat1 = p1.Latitude.ToRad(), lon1 = p1.Longitude.ToRad();
            double lat2 = p2.Latitude.ToRad(), lon2 = p2.Longitude.ToRad();
            double brng13 = brng1.ToRad(), brng23 = brng2.ToRad();
            double dLat = lat2-lat1, dLon = lon2-lon1;

            double dist12 = 2*Math.Asin( Math.Sqrt( Math.Sin(dLat/2)*Math.Sin(dLat/2) + 
              Math.Cos(lat1)*Math.Cos(lat2)*Math.Sin(dLon/2)*Math.Sin(dLon/2) ) );
            if (dist12 == 0) return null;

            // initial/final bearings between points
            double brngA = Math.Acos( ( Math.Sin(lat2) - Math.Sin(lat1)*Math.Cos(dist12) ) / 
              ( Math.Sin(dist12)*Math.Cos(lat1) ) );

            if (double.IsNaN(brngA)) brngA = 0; // protected against rounding

            double brngB = Math.Acos( ( Math.Sin(lat1) - Math.Sin(lat2)*Math.Cos(dist12) ) / 
              ( Math.Sin(dist12)*Math.Cos(lat2) ) );

            double brng12;
            double brng21;

            if (Math.Sin(lon2-lon1) > 0) {
              brng12 = brngA;
              brng21 = 2*Math.PI - brngB;
            } else {
              brng12 = 2*Math.PI - brngA;
              brng21 = brngB;
            }

            double alpha1 = (brng13 - brng12 + Math.PI) % (2*Math.PI) - Math.PI;  // angle 2-1-3
            double alpha2 = (brng21 - brng23 + Math.PI) % (2*Math.PI) - Math.PI;  // angle 1-2-3

            if (Math.Sin(alpha1)==0 && Math.Sin(alpha2)==0) return null;  // infinite intersections
            if (Math.Sin(alpha1)*Math.Sin(alpha2) < 0) return null;       // ambiguous intersection

            double alpha3 = Math.Acos( -Math.Cos(alpha1)*Math.Cos(alpha2) + 
                                 Math.Sin(alpha1)*Math.Sin(alpha2)*Math.Cos(dist12) );
            double dist13 = Math.Atan2(Math.Sin(dist12) * Math.Sin(alpha1) * Math.Sin(alpha2),
                                 Math.Cos(alpha2) + Math.Cos(alpha1) * Math.Cos(alpha3));
            double lat3 = Math.Asin( Math.Sin(lat1)*Math.Cos(dist13) + 
                              Math.Cos(lat1)*Math.Sin(dist13)*Math.Cos(brng13) );
            double dLon13 = Math.Atan2( Math.Sin(brng13)*Math.Sin(dist13)*Math.Cos(lat1), 
                                 Math.Cos(dist13)-Math.Sin(lat1)*Math.Sin(lat3) );
            double lon3 = lon1+dLon13;
            lon3 = (lon3+Math.PI) % (2*Math.PI) - Math.PI;  // normalise to -180..180º

            return new GeoPoint(lat3.ToDeg(), lon3.ToDeg());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MathHelperExtensions
    {
        public static double ToRad(this double value)
        {
            return value * Math.PI / 180;
        }

        public static double ToDeg(this double value)
        {
            return value * 180 / Math.PI;
        }
   
    }
}
