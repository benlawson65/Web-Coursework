using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NETboard.Startup))]
namespace NETboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
