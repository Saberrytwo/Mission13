using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {

        //BowlersDbContext _context { get; set; } //underscore because its a private variable
        private IBowlersRepository _repo { get; set; }
        //Constructor
        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;


        }


        public IActionResult Index(int ?TeamID)
        {
            ViewBag.Teams = _repo.Teams.ToList(); 
            if (TeamID is null)
            {
                ViewBag.TeamName = "Bowler Home Page";
                var blah = _repo.Bowlers.ToList();
                return View(blah);
            }
            else
            {
                var blah = _repo.Bowlers.Where(x => x.TeamID == TeamID).ToList();
                var team = _repo.Teams.Single(x => x.TeamID == TeamID);
                ViewBag.TeamName = team.TeamName;
                return View(blah);
            }

            
        }
        [HttpGet]
        public IActionResult BowlerEntry()
        {
            return View("EditBowler");
        }
        [HttpPost]
        public IActionResult BowlerEntry(Bowler b)
        {
            ViewBag.Teams = _repo.Teams.ToList();
            if (ModelState.IsValid)
            {
                _repo.CreateBowler(b);

            }
            else
            {
                return View("EditBowler", b);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int BowlerID)
        {
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == BowlerID);
            return View("EditBowler", bowler);
        }
        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            if (ModelState.IsValid)
            {
            ViewBag.Teams = _repo.Teams.ToList();
            _repo.UpdateBowler(b);
            var blah = _repo.Bowlers.ToList();
            return RedirectToAction("Index");
            }
            else
            {
                return View("EditBowler", b);
            }

        }

        public IActionResult Delete(int BowlerID)
        {
            ViewBag.Teams = _repo.Teams.ToList();
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == BowlerID);
            _repo.DeleteBowler(bowler);
            return RedirectToAction("Index");
        }

    }
}
