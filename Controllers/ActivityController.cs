using System;
using System.Linq;
using DojoActivities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoActivities.Controllers
{
    public class ActivityController : Controller
    {

        private ActivityContext _context;
        private readonly DbConnector _dbConnector;

        public ActivityController(DbConnector connect, ActivityContext context)
        {
            _dbConnector = connect;
            _context = context;
        }

        private bool isLoggedIn()
        {
            int? currId = HttpContext.Session.GetInt32("currUser");
            if(currId == null)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(!isLoggedIn()) return Redirect("/");
            User currUser = _context.Users.Single(user => user.UserId == (int)HttpContext.Session.GetInt32("currUser"));
            ViewBag.CurrentUser = currUser;
            ViewBag.AllActivities = _context.Activities.Include(act => act.Creator).Include(act => act.Signedup).ToList();
            ViewBag.CurrUser = (int)HttpContext.Session.GetInt32("currUser");
            return View("Dashboard");
        }

        [HttpGet]
        [Route("newactivity")]
        public IActionResult NewActivity()
        {
            if(!isLoggedIn()) return Redirect("/");
            return View("NewActivity");
        }

        [HttpPost]
        [Route("createactivity")]
        public IActionResult CreateActivity(ViewActivity newActivity)
        {
            if(!isLoggedIn()) return Redirect("/");
            if(ModelState.IsValid)
            {
                Activity activity = new Activity
                {
                    Title = newActivity.Title,
                    TimeAndDate = new DateTime(newActivity.Date.Year, newActivity.Date.Month, newActivity.Date.Day, newActivity.Time.Hour, newActivity.Time.Minute, 0),
                    Duration = newActivity.Duration,
                    Description = newActivity.Description,
                    Creator = _context.Users.Single(user => user.UserId == (int)HttpContext.Session.GetInt32("currUser")),
                    CreatorId = (int)HttpContext.Session.GetInt32("currUser")
                };
                _context.Add(activity);
                _context.SaveChanges();
                activity.created_at = DateTime.Now;
                activity.updated_at = DateTime.Now;
                _context.SaveChanges();
                return Redirect("/show/" + activity.ActivityId);
            }
            return View("NewActivity", newActivity);
        }

        [HttpGet]
        [Route("show/{id}")]
        public IActionResult ShowActivity(int id)
        {
            Activity test;
            if(!isLoggedIn()) return Redirect("/");
            try
            {
                test = _context.Activities.Include(act => act.Creator).Include(act => act.Signedup).ThenInclude(su => su.User).Single(act => act.ActivityId == id);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            ViewBag.Activity = test;
            ViewBag.CurrUser = (int)HttpContext.Session.GetInt32("currUser");
            return View("Show");
        }

        [HttpGet]
        [Route("join/{id}")]
        public IActionResult SignUp(int id)
        {
            Activity test;
            if(!isLoggedIn()) return Redirect("/");
            try
            {
                test = _context.Activities.Include(act => act.Creator).Include(act => act.Signedup).ThenInclude(su => su.User).Single(act => act.ActivityId == id);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            if(test.CreatorId == (int)HttpContext.Session.GetInt32("currUser"))
            {
                return RedirectToAction("Dashboard");
            }
            Signup signup = new Signup
            {
                UserId = (int)HttpContext.Session.GetInt32("currUser"),
                ActivityId = test.ActivityId
            };
            _context.Signups.Add(signup);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("leave/{id}")]
        public IActionResult Leave(int id)
        {
            Activity test;
            if(!isLoggedIn()) return Redirect("/");
            try
            {
                test = _context.Activities.Include(act => act.Creator).Include(act => act.Signedup).ThenInclude(su => su.User).Single(act => act.ActivityId == id);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            int cu = (int)HttpContext.Session.GetInt32("currUser");
            Signup toRemove;
            try
            {
                toRemove = _context.Signups.Single(su => su.ActivityId == test.ActivityId && su.UserId == cu);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            _context.Signups.Remove(toRemove);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Activity test;
            if(!isLoggedIn()) return Redirect("/");
            try
            {
                test = _context.Activities.Include(act => act.Creator).Include(act => act.Signedup).ThenInclude(su => su.User).Single(act => act.ActivityId == id);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            int cu = (int)HttpContext.Session.GetInt32("currUser");
            Activity toRemove;
            try
            {
                toRemove = _context.Activities.Single(act => act.ActivityId == id);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            _context.Activities.Remove(toRemove);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}