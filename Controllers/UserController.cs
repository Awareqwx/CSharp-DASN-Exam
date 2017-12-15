using System;
using System.Linq;
using DojoActivities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DojoActivities.Controllers
{
    public class UserController : Controller
    {

        private ActivityContext _context;
        private readonly DbConnector _dbConnector;

        public UserController(DbConnector connect, ActivityContext context)
        {
            _dbConnector = connect;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(ViewUser newUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Where(user => user.Email == newUser.Email).Count() != 0)
                {
                    ModelState.AddModelError("Email", "Email already exists!");
                }
                else
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    User user = new User
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email
                    };
                    user.PasswordHash = hasher.HashPassword(user, newUser.Password);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    user.created_at = DateTime.Now;
                    user.updated_at = DateTime.Now;
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("currUser", user.UserId);
                    return Redirect("/dashboard");
                }
            }
            return View("Index", newUser);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User test;
            try
            {
                test = _context.Users.Single(user => user.Email == email);
            }
            catch
            {
                ViewBag.LoginError = "Email does not exist";
                return View("Index");
            }
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            if(hasher.VerifyHashedPassword(test, test.PasswordHash, password) == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetInt32("currUser", test.UserId);
                return Redirect("/dashboard");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}