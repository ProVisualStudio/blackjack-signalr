using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlackJack.Startup))]
namespace BlackJack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
