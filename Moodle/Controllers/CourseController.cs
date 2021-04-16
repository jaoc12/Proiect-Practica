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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            Course course = new Course();
            course.ProfessorsList = GetProfessors();
            course.StudentsList = GetAllStudents();
            course.Students = new List<Student>();
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Course courseRequest)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Name = courseRequest.Name,
                    ProfessorId = courseRequest.ProfessorId
                };
                course.Students = new List<Student>();
                var selectedStudents = courseRequest.StudentsList.Where(s => s.Checked).ToList();
                for (int i = 0; i < selectedStudents.Count(); i++)
                {
                    Student student = db.Students.Find(selectedStudents[i].Id);
                    course.Students.Add(student);
                }
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = course.CourseId });
            }
            return RedirectToAction("New");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AssignStudent()
        {
            Course course = new Course();
            course.CoursesList = GetCourses();
            course.StudentsList = GetAllStudents();
            course.Students = new List<Student>();
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AssignStudent(Course courseRequest)
        {
            if (ModelState.IsValid)
            {
                Course course = db.Courses.Find(courseRequest.CourseId);
                var selectedStudents = courseRequest.StudentsList.Where(s => s.Checked).ToList();
                if (TryUpdateModel(course))
                {

                    course.Students.Clear();
                    course.Students = new List<Student>();
                    for (int i = 0; i < selectedStudents.Count(); i++)
                    {
                        Student student = db.Students.Find(selectedStudents[i].Id);
                        course.Students.Add(student);
                    }

                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Course", new { id = course.CourseId });
            }
            return RedirectToAction("AssignStudent");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ChangeProfessor()
        {
            Course course = new Course();
            course.CoursesList = GetCourses();
            course.ProfessorsList = GetProfessors();
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ChangeProfessor(Course courseRequest)
        {
            if (ModelState.IsValid)
            {
                Course course = db.Courses.Find(courseRequest.CourseId);
                if (TryUpdateModel(course))
                {
                    course.ProfessorId = courseRequest.ProfessorId;
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Course", new { id = course.CourseId });
            }
            return RedirectToAction("ChangeProfessor");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetProfessors()
        {
            var selectList = new List<SelectListItem>();
            foreach (var professor in db.Professors.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = professor.ProfessorId.ToString(),
                    Text = professor.ToString()
                });
            }
            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetCourses()
        {
            var selectList = new List<SelectListItem>();
            foreach (var course in db.Courses.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = course.CourseId.ToString(),
                    Text = course.Name
                });
            }
            return selectList;
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllStudents()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var student in db.Students.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = student.StudentId,
                    Name = student.ToString(),
                    Checked = false
                });
            }
            return checkboxList;
        }
    }
}