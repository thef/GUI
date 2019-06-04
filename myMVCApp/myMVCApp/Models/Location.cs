using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myMVCApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoadName { get; set; }
        public int RoadNumber { get; set; }
        public int PostNumber { get; set; }
        public string City { get; set; }

        public List<Tree> Trees { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}
