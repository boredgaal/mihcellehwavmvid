namespace Mihcelle.Hwavmvid
{
    public interface Programinterface
    {

        Task Configure(IServiceCollection services);
        Task Configureapp(WebApplication app);

    }
}
