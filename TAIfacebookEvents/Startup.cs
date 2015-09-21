using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TAIfacebookEvents.Startup))]
namespace TAIfacebookEvents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
