using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int StudyYear { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public new String ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}