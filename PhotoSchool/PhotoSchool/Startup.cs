using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoSchool.Startup))]
namespace PhotoSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
