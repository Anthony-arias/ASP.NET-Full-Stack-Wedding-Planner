using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wedding_Planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Wedding_Planner.Controllers
{
    public class UsersController : Controller
    {
        private Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [HttpPost("user/create")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    _context.Add(user);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("CurrentUser", user.UserId);
                    return RedirectToAction("ViewDashboard", "Home");
                }
            }
            return View("Index");
        }

        
        [HttpPost("user/login")]
        public IActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var ThisUser = _context.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
                if (ThisUser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }

                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, ThisUser.Password, user.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.SetInt32("CurrentUser", ThisUser.UserId);
                    return RedirectToAction("ViewDashboard", "Home");
                }
            }
            return View("Index");
        }
    }
}
