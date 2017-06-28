using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Infnet.DotnetProject.Assessment.Presentation.Startup))]
namespace Infnet.DotnetProject.Assessment.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
