using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class LocationTree
    {
        //Many to Many
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int TreeId { get; set; }
        public Tree Tree { get; set; }

        [NotMapped]
        public List<SelectListItem> listLocations { get; set; }

        [NotMapped]
        public List<SelectListItem> listTrees { get; set; }
    }
}