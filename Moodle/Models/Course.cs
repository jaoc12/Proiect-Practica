using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public int? ProfessorId { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ProfessorsList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> CoursesList { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> StudentsList { get; set; }
    }
}