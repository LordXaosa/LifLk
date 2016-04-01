using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LifLk.Startup))]
namespace LifLk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
