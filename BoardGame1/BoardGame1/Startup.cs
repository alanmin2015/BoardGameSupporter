using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoardGame1.Startup))]
namespace BoardGame1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
