using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppGestionEMS.Startup))]
namespace AppGestionEMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
