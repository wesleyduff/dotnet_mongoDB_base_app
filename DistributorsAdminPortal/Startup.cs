using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DistributorsAdminPortal.Startup))]
namespace DistributorsAdminPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
