using Mihcelle.Hwavmvid.Modules.Roulette;

namespace Mihcelle.Hwavmvid.Modules.Roulette
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterface
    {

        public async Task Configure(IServiceCollection services)
        {

            services.AddScoped<Applicationdbcontext, Applicationdbcontext>();

        }

        public async Task Configureapp(WebApplication application)
        {

        }

    }
}
