using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle.Models
{
    public class Section
    {
        public int SectionId { get; set; }

        public string Name { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}