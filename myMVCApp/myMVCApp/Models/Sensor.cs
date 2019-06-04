using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myMVCApp.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string WoodArt { get; set; }
        public string SensorId { get; set; }
        public double GpsCoordinateLat { get; set; }
        public double GpsCoordinateLon { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
