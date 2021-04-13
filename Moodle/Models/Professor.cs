using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle.Models
{
    public class Professor
    {
        public int ProfessorId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string GradDidactic { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public new String ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}