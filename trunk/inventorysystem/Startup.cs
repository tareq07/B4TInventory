using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inventorysystem.Startup))]
namespace inventorysystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
