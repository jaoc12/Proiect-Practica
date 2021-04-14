using Microsoft.AspNet.Identity;
using Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Controllers
{
    public class StudentController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Student student = db.Students.Find(id);

                if (student == null)
                {
                    return HttpNotFound("Could not find the student with id " + id.ToString() + "!");
                }

                return View(student);
            }
            return HttpNotFound("Missing student id parameter!");
        }

        [Authorize(Roles = "Student, Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Student student = db.Students.Find(id);

                if (student == null)
                {
                    return HttpNotFound("Could not find the student with id " + id.ToString() + "!");
                }

                if (!User.IsInRole("Admin"))
                {
                    if (!student.UserId.Equals(User.Identity.GetUserId()))
                    {
                        return HttpNotFound("The current user can't edit the student with the id" + id);
                    }
                }

                return View(student);
            }
            return HttpNotFound("Missing student id parameter!");
        }

        [Authorize(Roles = "Student, Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Student studentRequestt)
        {
            Student student = db.Students.Find(id);

            if (ModelState.IsValid)
            {
                if (TryUpdateModel(student))
                {
                    student.FirstName = studentRequestt.FirstName;
                    student.LastName = studentRequestt.LastName;
                    student.StudyYear = studentRequestt.StudyYear;

                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Student", new { id = id });
            }
            return View(studentRequestt);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Student student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return HttpNotFound("Could not find the student with id " + id.ToString() + "!");
        }
    }
}