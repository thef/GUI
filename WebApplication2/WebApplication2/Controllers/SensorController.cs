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
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SensorController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sensors =  _db.Sensors.ToList();
            return View("~/Views/Sensors/Index.cshtml", sensors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Sensors/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Store([Bind("Id, WoodArt, SensorId, GpsCoordinateLat, GpsCoordinateLon")] Location location)
        {
            if (ModelState.IsValid)
            {
                var selectLocation = _db.Locations.FirstOrDefault(l => l.Id.Equals(location.Id));

                //Add object to database & save changes.
                _db.Sensors.Add(new Sensor{
                    Location = selectLocation,
                });

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }

    }
}
