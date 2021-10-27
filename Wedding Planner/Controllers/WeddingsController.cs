using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wedding_Planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Wedding_Planner.Controllers
{
    public class WeddingsController : Controller
    {
        private Context _context;

        public WeddingsController(Context context)
        {
            _context = context;
        }

        [HttpPost("wedding/create")]
        public IActionResult CreateWedding(Wedding wedding)
        {
            if (ModelState.IsValid)
            {
                if (wedding.WeddingDate.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("WeddingDate", "Wedding must be in the future!");
                    return View("WeddingForm");
                }
                else
                {
                    _context.Add(wedding);
                    _context.SaveChanges();
                    return RedirectToAction("ViewWeddingInfo", "Home", new { id = wedding.WeddingId });
                }
            }
            return View("WeddingForm");
        }

        [HttpGet("wedding/addAttendee")]
        public IActionResult AddAttendeeToWedding(int WeddingId, int UserId)
        {
            Console.WriteLine(WeddingId);
            Console.WriteLine(UserId);
            _context.Add(new WeddingUserAssociation { WeddingId = WeddingId, UserId = UserId });
            _context.SaveChanges();
            return RedirectToAction("ViewDashboard", "Home");
        }

        [HttpGet("wedding/removeAttendee")]
        public IActionResult removeAttendeeFromWedding(int WeddingId, int UserId)
        {
            WeddingUserAssociation thisAssociation = _context.WeddingUserAssociation
                .FirstOrDefault(This => This.WeddingId == WeddingId && This.UserId == UserId);

            _context.Remove(thisAssociation);
            _context.SaveChanges();
            return RedirectToAction("ViewDashboard", "Home");
        }

        [HttpGet("wedding/delete")]
        public IActionResult RemoveWedding(int WeddingId)
        {
            Wedding ThisWedding = _context.Weddings
                .FirstOrDefault(wedding => wedding.WeddingId == WeddingId);

            _context.Remove(ThisWedding);
            _context.SaveChanges();
            return RedirectToAction("ViewDashboard", "Home");
        }

    }
}
