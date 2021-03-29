using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}