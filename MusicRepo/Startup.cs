using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicRepo.Startup))]
namespace MusicRepo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
