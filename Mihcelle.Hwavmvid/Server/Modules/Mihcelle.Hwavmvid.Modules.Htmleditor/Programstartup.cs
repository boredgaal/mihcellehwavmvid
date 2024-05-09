namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterface
    {

        public async Task Configure(IServiceCollection services)
        {

            services.AddScoped<Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationdbcontext, Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationdbcontext>();

        }

        public async Task Configureapp(WebApplication application)
        {

        }

    }
}
