using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Memorizer.Startup))]
namespace Memorizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
