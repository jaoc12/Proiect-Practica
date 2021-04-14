using Microsoft.AspNet.Identity;
using Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Controllers
{
    public class ProfessorController : Controller
    {
        private DbCtx db = new DbCtx();
        // GET: Professor
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Professor professor = db.Professors.Find(id);

                if (professor == null)
                {
                    return HttpNotFound("Could not find the professor with id " + id.ToString() + "!");
                }

                if (!User.IsInRole("Admin"))
                {
                    if (!professor.UserId.Equals(User.Identity.GetUserId()))
                    {
                        return HttpNotFound("The current user can't edit the professor with the id" + id);
                    }
                }

                return View(professor);
            }
            return HttpNotFound("Missing professor id parameter!");
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Professor professorRequest)
        {
            Professor professor = db.Professors.Find(id);

            if (ModelState.IsValid)
            {
                if (TryUpdateModel(professor))
                {
                    professor.FirstName = professorRequest.FirstName;
                    professor.LastName = professorRequest.LastName;
                    professor.GradDidactic = professorRequest.GradDidactic;

                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Professor", new { id = id });
            }
            return View(professorRequest);
        }

        [HttpGet]
        public ActionResult AllProfessors()
        {
            List<Professor> professors = db.Professors.OrderBy(p => p.FirstName).ToList();
            return View(professors);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Professor professor = db.Professors.Find(id);

                if (professor == null)
                {
                    return HttpNotFound("Could not find the professor with id " + id.ToString() + "!");
                }

                return View(professor);
            }
            return HttpNotFound("Missing professor id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Professor professor = db.Professors.Find(id);
            if (professor != null)
            {
                foreach(File file in professor.Files.ToList())
                {
                    db.Files.Remove(file);
                }
                foreach(Course course in professor.Courses.ToList())
                {
                    db.Courses.Remove(course);
                }
                db.Professors.Remove(professor);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return HttpNotFound("Could not find the professor with id " + id.ToString() + "!");
        }
    }
}