using Moodle.Models.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Moodle.Models
{
    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Section> Sections { get; set; }
    }
}