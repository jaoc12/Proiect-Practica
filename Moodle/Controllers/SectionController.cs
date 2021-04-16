using Microsoft.AspNet.Identity;
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

        [Authorize(Roles = "Professor")]
        [HttpGet]
        public ActionResult New(int? id)
        {
            if (id.HasValue)
            {
                Course course = db.Courses.Find(id);
                if (course != null)
                {
                    if (course.Professor.UserId.Equals(User.Identity.GetUserId()))
                    {
                        Section section = new Section { CourseId = (int)id };
                        return View(section);
                    }
                    return HttpNotFound("The current professor doesn't have access to the course " + course.Name);
                }
                return HttpNotFound("Wrong course id parameter!");
            }
            return HttpNotFound("Missing course id parameter!");
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult New(Section sectionRequest)
        {
            if (ModelState.IsValid)
            {
                Section section = new Section
                {
                    Name = sectionRequest.Name,
                    CourseId = sectionRequest.CourseId
                };
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Details", "Course", new { id = sectionRequest.CourseId });
            }
            return RedirectToAction("New");
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

        [Authorize(Roles = "Professor, Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Section section = db.Sections.Find(id);

                if (section == null)
                {
                    return HttpNotFound("Could not find the section with id " + id.ToString() + "!");
                }

                if (!User.IsInRole("Admin"))
                {
                    if (!section.Course.Professor.UserId.Equals(User.Identity.GetUserId()))
                    {
                        return HttpNotFound("The current user can't edit the section with the id: " + id);
                    }
                }

                return View(section);
            }
            return HttpNotFound("Missing section id parameter!");
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Section sectionRequest)
        {
            Section section = db.Sections.Find(id);

            if (ModelState.IsValid)
            {
                if (TryUpdateModel(section))
                {
                    section.Name = sectionRequest.Name;
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Section", new { id = id });
            }
            return View(sectionRequest);
        }

        [Authorize(Roles = "Professor,Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Section section = db.Sections.Find(id);
            if (section != null)
            {
                int courseId = section.CourseId;
                foreach (File file in section.Files.ToList())
                {
                    db.Files.Remove(file);
                }

                db.Sections.Remove(section);
                db.SaveChanges();

                return RedirectToAction("Details", "Course", new { id = courseId });
            }
            return HttpNotFound("Could not find the section with id " + id.ToString() + "!");
        }
    }
}