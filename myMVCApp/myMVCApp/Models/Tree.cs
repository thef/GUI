using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myMVCApp.Models
{
    public class Tree
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
