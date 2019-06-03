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
    public class TreeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TreeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var trees =  _db.Trees.ToList();
            return View("~/Views/Trees/Index.cshtml", trees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Trees/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Store([Bind("Id, Amount, Name")] Tree tree)
        {
            if (ModelState.IsValid)
            {
                _db.Add(tree);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }

    }
}
