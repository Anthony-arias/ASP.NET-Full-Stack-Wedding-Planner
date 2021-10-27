using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wedding_Planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Wedding_Planner.Controllers
{
    public class AllViewModel
    {
        public List<User> AllUser { get; set; }
        public List<Wedding> AllWeddings { get; set; }
        public User ThisUser { get; set; }
        public Wedding ThisWedding { get; set; }
    }

    public class HomeController : Controller
    {
        private Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }

        // View Routers start here
        [HttpGet("")] // Login Reg Form
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet("dashboard")]
        public ViewResult ViewDashboard()
        {
            if(!HttpContext.Session.GetInt32("CurrentUser").HasValue)
            {
                return View("Index");
            }
            List<Wedding> AllWeddings = _context.Weddings
                .Include(wedding => wedding.Attendees)
                .ThenInclude(wedding => wedding.User)
                .ToList();
            TempData["LogedUser"] = (int)HttpContext.Session.GetInt32("CurrentUser");
            return View("Dashboard", AllWeddings);
        }

        [HttpGet("wedding/{id}/info")]
        public ViewResult ViewWeddingInfo(int id)
        {
            if (!HttpContext.Session.GetInt32("CurrentUser").HasValue)
            {
                return View("Index");
            }
            Wedding ThisWedding = _context.Weddings
                .Include(wedding => wedding.Attendees)
                .ThenInclude(wedding => wedding.User)
                .FirstOrDefault(wedding => wedding.WeddingId == id);
            return View("WeddingInfo", ThisWedding);
        }

        [HttpGet("wedding/create")]
        public ViewResult ViewWeddingForm()
        {
            if (!HttpContext.Session.GetInt32("CurrentUser").HasValue)
            {
                return View("Index");
            }
            TempData["LogedUser"] = (int)HttpContext.Session.GetInt32("CurrentUser");
            return View("WeddingForm");
        }
        // View Routers end here

    }
}
