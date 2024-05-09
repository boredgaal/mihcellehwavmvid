using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;
using Mihcelle.Hwavmvid.Pager;
using Mihcelle.Hwavmvid.Server.Tasks;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Taskcontroller : ControllerBase
    {


        public Applicationdbcontext applicationdbcontext { get; set; }
        public IServiceScopeFactory servicescopefactory { get; set; }

        public Taskcontroller(Applicationdbcontext applicationdbcontext, IServiceScopeFactory iservicescopefactory)
        {
            this.applicationdbcontext = applicationdbcontext;
            this.servicescopefactory = iservicescopefactory;
        }

        [Authorize]
        [HttpGet("{contextpage}/{itemsperpage}/{siteid}")]
        public async Task<Pagerapiitem<Applicationtask>?> Get(int contextpage, int itemsperpage, string siteid)
        {


            try
            {
                var scope = this.servicescopefactory.CreateScope();
                var hostedservices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(IHostedservicebase)).IsAssignableFrom(assemblytypes));
                var tasks = new List<Applicationtask>();

                foreach (var serviceclassitem in hostedservices)
                {

                    if (serviceclassitem.IsClass)
                    {

                        var hostedserviceitem = (IHostedservicebase?)scope.ServiceProvider.GetService(serviceclassitem);
                        if (hostedserviceitem != null)
                        {

                            Applicationtask item = new Applicationtask()
                            {
                                Id = hostedserviceitem.Id,
                                Taskname = hostedserviceitem.Taskname,
                                Projectname = hostedserviceitem.Projectname,
                                Interval = hostedserviceitem.Interval,
                                Active = hostedserviceitem.Active,
                            };
                            tasks.Add(item);
                        }
                    }
                }

                var items = tasks.Skip((contextpage - 1) * itemsperpage).Take(itemsperpage).ToList();
                int pagesTotal = Convert.ToInt32(Math.Ceiling(tasks.Count() / Convert.ToDouble(itemsperpage)));
                var apiitem = new Pagerapiitem<Applicationtask>()
                {
                    Items = items,
                    Pages = pagesTotal,
                };

                return apiitem;
            }
            catch (Exception exception) { return null; }
        }

        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] Applicationtask taskitem)
        {


            try
            {
                var scope = this.servicescopefactory.CreateScope();
                var hostedservices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(IHostedservicebase)).IsAssignableFrom(assemblytypes));

                foreach (var serviceclassitem in hostedservices)
                {

                    if (serviceclassitem.IsClass)
                    {

                        var hostedserviceitem = (IHostedservicebase?)scope.ServiceProvider.GetService(serviceclassitem);
                        if (hostedserviceitem != null && hostedserviceitem.Id == taskitem.Id)
                        {
                            hostedserviceitem.Active = taskitem.Active;
                            break;
                        }
                    }
                }
            }
            catch (Exception exception) { throw new NotImplementedException(); }

        }

    }
}
