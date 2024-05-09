using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Modules.Blackjack;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Modulesettingscontroller
    {

        public Applicationdbcontext applicationdbcontext {  get; set; }

        public Modulesettingscontroller(Applicationdbcontext database)
        {
            this.applicationdbcontext = database;
        }

        [Authorize]
        [HttpGet("{moduleid}")]
        public async Task<Dictionary<string, string>> Get(string moduleid)
        {
            var items = await this.applicationdbcontext.Applicationmodulesettings.Where(item => item.Moduleid == moduleid).ToListAsync();
            Dictionary<string, string> modulesettings = new Dictionary<string, string>();
            foreach (var item in items)
            {
                modulesettings.Add(item.Key, item.Value);
            }

            return modulesettings;
        }

        [Authorize]
        [HttpGet("{moduleid}/{itemkey}/{itemvalue}")]
        public async Task Get(string moduleid, string itemkey, string itemvalue)
        {

            var modulesettingsitem = await this.applicationdbcontext.Applicationmodulesettings.FirstOrDefaultAsync(item => item.Key == itemkey);
            if (modulesettingsitem != null)
            {
                modulesettingsitem.Value = itemvalue;
                this.applicationdbcontext.Applicationmodulesettings.Update(modulesettingsitem);
                await this.applicationdbcontext.SaveChangesAsync();
            }
            else
            {
                var item = new Applicationmodulesettings()
                {
                    Moduleid = moduleid,
                    Key = itemkey,
                    Value = itemvalue,
                    Createdon = DateTime.Now,
                };
                await this.applicationdbcontext.Applicationmodulesettings.AddAsync(item);
                await this.applicationdbcontext.SaveChangesAsync();
            }            
        }

    }
}
