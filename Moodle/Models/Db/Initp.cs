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
            Course course = new Course
            {
                CourseId = 1,
                Name = "Info"
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