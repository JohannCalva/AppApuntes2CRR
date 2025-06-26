using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApuntes2CRR.Models
{
    public class WeatherModels
    {
    }
    public class WeatherData
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public double utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public CurrentUnits current_units { get; set; }
        public Current current { get; set; }
    }

    public class CurrentUnits
    {
        public string time { get; set; }
        public string interval { get; set; }
        public string temperature_2m { get; set; }
        public string relative_humidity_2m { get; set; }
        public string rain { get; set; }
    }

    public class Current
    {
        public string time { get; set; }
        public double interval { get; set; }
        public double temperature_2m { get; set; }
        public double relative_humidity_2m { get; set; }
        public double rain { get; set; }
    }
}
