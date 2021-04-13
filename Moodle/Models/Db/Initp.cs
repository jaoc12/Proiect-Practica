using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Moodle.Models.Db
{
    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx context)
        {
            Professor professor = new Professor
            {
                ProfessorId = 1,
                FirstName = "Professor",
                LastName = "Professorson",
                GradDidactic = "Ceva",
                UserId = "e28a3fab-b9ea-4901-9ee2-06ec730efa6c"
            };

            context.Professors.Add(professor);

            Student student = new Student
            {
                StudentId = 1,
                FirstName = "Student",
                LastName = "Studenson",
                StudyYear = 1,
                UserId = "2a03fc9c-df96-4ed2-a944-275834091549"
            };

            context.Students.Add(student);

            Course course = new Course
            {
                CourseId = 1,
                Name = "Info",
                Professor = professor,
                Students = new List<Student> { student}
            };

            context.Courses.Add(course);

            Section section1 = new Section
            {
                Name = "Curs",
                CourseId = 1
            };

            Section section2 = new Section
            {
                Name = "Laborator",
                CourseId = 1
            };

            context.Sections.Add(section1);
            context.Sections.Add(section2);

            base.Seed(context);
        }
    }
}