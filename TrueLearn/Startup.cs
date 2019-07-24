using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrueLearn.Startup))]
namespace TrueLearn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
