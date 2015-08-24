using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewLearning_V0._1_.Startup))]
namespace NewLearning_V0._1_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
