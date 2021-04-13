using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle.Models
{
    public class File
    {
        public int FileId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Uploader { get; set; }

        public DateTime UploadDate { get; set; }

        public byte[] Content { get; set; }

        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
    }
}