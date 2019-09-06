using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Kladilnica.Startup))]
namespace Web_Kladilnica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
