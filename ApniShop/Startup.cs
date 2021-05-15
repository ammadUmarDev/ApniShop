using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApniShop.App_Start.StartUp))]
namespace ApniShop.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
