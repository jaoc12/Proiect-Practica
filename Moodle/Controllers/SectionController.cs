using Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Controllers
{
    public class SectionController : Controller
    {
        private DbCtx db = new DbCtx();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Section section = db.Sections.Find(id);
                if (section != null)
                {
                    return View(section);
                }
                return HttpNotFound("Could not find the course with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing course id parameter!");
        }
    }
}