using Microsoft.AspNet.Identity;
using Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Controllers
{
    public class HomeController : Controller
    {
        private DbCtx db = new DbCtx();

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (User.IsInRole("Professor"))
            {
                var userId = User.Identity.GetUserId();
                Professor professor = db.Professors.Where(p => p.UserId.Equals(userId)).FirstOrDefault();
                return RedirectToAction("Details", "Professor", new { id = professor.ProfessorId });
            }
            if (User.IsInRole("Student"))
            {
                var userId = User.Identity.GetUserId();
                Student student = db.Students.Where(s => s.UserId.Equals(userId)).FirstOrDefault();
                return RedirectToAction("Details", "Student", new { id = student.StudentId });
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}