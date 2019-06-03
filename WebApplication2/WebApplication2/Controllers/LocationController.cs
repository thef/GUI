using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LocationController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var locations =  _db.Locations.ToList();
            return View("~/Views/Locations/Index.cshtml", locations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Locations/Create.cshtml");
        }

        [HttpGet]
        public IActionResult Show(string locationName)
        {
            if (locationName == null)
            {
                return NotFound();
            }

            var locations =  _db.Locations.Where(x => x.Name == locationName).ToList();
            return View("~/Views/Locations/Index.cshtml", locations);
        }

        [HttpPost]
        public IActionResult Store([Bind("Id, Name, RoadName, RoadNumber, PostNumber, City")] Location location)
        {
            if (ModelState.IsValid)
            {
                _db.Add(location);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }

    }
}
