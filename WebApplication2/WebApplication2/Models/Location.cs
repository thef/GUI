using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoadName { get; set; }
        public int RoadNumber { get; set; }
        public int PostNumber { get; set; }
        public string City { get; set; }

        public List<Tree> Tree { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}