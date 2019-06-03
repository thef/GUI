using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LocationTreeController : Controller
    {
        private readonly ApplicationDbContext _db;


        public LocationTreeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int locationId)
        {
            var locationsTrees = _db.LocationTrees.Include(x => x.Location).Include(x => x.Tree).Where(x => x.LocationId == locationId).ToList();
            return View("~/Views/LocationTrees/Index.cshtml", locationsTrees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //Create SelectList boxes.
            var model = new LocationTree();

            List<SelectListItem> listLocation = new List<SelectListItem>();   
            foreach (var location in _db.Locations)
            {
                listLocation.Add(new SelectListItem() { Value = location.Id.ToString(), Text = location.Name });
            }
            model.listLocations = listLocation;

            List<SelectListItem> listTree = new List<SelectListItem>();  
            foreach (var tree in _db.Trees)
            {
                listTree.Add(new SelectListItem() { Value = tree.Id.ToString(), Text = tree.Name.ToString() });
            }
            model.listTrees = listTree;

            return View("~/Views/LocationTrees/Create.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store([Bind("LocationId, TreeId")] LocationTree locationTree)
        {
        
            if (ModelState.IsValid)
            {
                var selectTree = _db.Trees.Single(t => t.Id.Equals(locationTree.LocationId));

                var selectLocation = _db.Locations.Single(l => l.Id.Equals(locationTree.TreeId));

                //Add object to database & save changes.
                _db.LocationTrees.Add(new LocationTree{
                    Location = selectLocation,
                    Tree = selectTree
                });

                _db.SaveChanges();

                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Create));
        }

    }
}
