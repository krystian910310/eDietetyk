using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eDietetyk.Startup))]
namespace eDietetyk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
