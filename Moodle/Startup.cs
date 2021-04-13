using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Moodle.Models;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Moodle.Startup))]
namespace Moodle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CheckAdminandRoles();
        }

        private void CheckAdminandRoles()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            if (!roleManager.RoleExists("Admin"))
            {
                // adaugam rolul de administrator
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                var adminCreated = userManager.Create(user, "Admin!");
                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Professor"))
            {
                var role = new IdentityRole();
                role.Name = "Professor";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }
        }
    }
}
