using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        public string WoodArt { get; set; }
        public string SensorId { get; set; }
        public double GpsCoordinateLat { get; set; }
        public double GpsCoordinateLon { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}