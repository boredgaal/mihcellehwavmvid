
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;

namespace Mihcelle.Hwavmvid.Server.Tasks
{
    public class Hostedservicebase : IHostedService
    {



        public IServiceScopeFactory servicescopefactory { get; set; }
        public Applicationdbcontext frameworkapplicationdbcontext { get; set; }

        public Hostedservicebase(IServiceScopeFactory servicescopefactory)
        {
            this.servicescopefactory = servicescopefactory;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {

            var scope = this.servicescopefactory.CreateScope();
            this.frameworkapplicationdbcontext = scope.ServiceProvider.GetService<Applicationdbcontext>();

            var hostedservices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(IHostedservicebase)).IsAssignableFrom(assemblytypes));
            foreach (var serviceclassitem in hostedservices)
            {
                try
                {
                    if (serviceclassitem.IsClass)
                    {

                        var hostedserviceitem = (IHostedservicebase?) scope.ServiceProvider.GetService(serviceclassitem);
                        if (hostedserviceitem != null)
                        {
                            Task task = Task.Run(async () =>
                            {
                                while (true)
                                {
                                    hostedserviceitem.Runtaskimplementation(this.frameworkapplicationdbcontext);
                                    await Task.Delay(hostedserviceitem.Interval);
                                }                                
                            });
                        }

                    }
                }
                catch (Exception exception) { 
                    Console.WriteLine(exception.Message); }
            }
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

    }
}
