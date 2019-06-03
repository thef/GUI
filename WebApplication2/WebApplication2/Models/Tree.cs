using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Tree
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }       
    }
}