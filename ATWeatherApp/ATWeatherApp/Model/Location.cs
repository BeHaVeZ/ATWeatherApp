using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWeatherApp.Model
{
    public class Metric
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Elevation
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class GeoPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Elevation Elevation { get; set; }
    }

    public class Location
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public GeoPosition GeoPosition { get; set; }
    }

}
