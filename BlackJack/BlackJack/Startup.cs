using BlackJack.Models;
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
            app.MapSignalR();
            Card[] Cards = new Card[52] ;
            Cards[0].;
        }
    }
}
