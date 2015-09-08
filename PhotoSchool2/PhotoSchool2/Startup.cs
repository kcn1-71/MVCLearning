using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoSchool2.Startup))]
namespace PhotoSchool2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
