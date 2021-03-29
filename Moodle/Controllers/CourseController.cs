using Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Controllers
{
    public class CourseController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Course
        public ActionResult Index()
        {
            List<Course> courses = db.Courses.ToList();
            ViewBag.Courses = courses;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Course course = db.Courses.Find(id);
                if (course != null)
                {
                    return View(course);
                }
                return HttpNotFound("Could not find the course with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing course id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Course course = new Course();
            return View(course);
        }
    }
}