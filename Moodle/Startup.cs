using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moodle.Startup))]
namespace Moodle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
