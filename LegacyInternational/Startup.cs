using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LegacyInternational.Startup))]
namespace LegacyInternational
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
