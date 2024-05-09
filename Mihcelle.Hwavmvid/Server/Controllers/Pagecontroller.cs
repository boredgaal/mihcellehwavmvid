using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;
using Mihcelle.Hwavmvid.Pager;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Pagecontroller : ControllerBase
    {

        public IWebHostEnvironment iwebhostenvironment { get; set; }
        public Applicationdbcontext applicationdbcontext { get; set; }

        public Pagecontroller(IWebHostEnvironment iwebhostenvironment, Applicationdbcontext applicationdbcontext)
        {
            this.iwebhostenvironment = iwebhostenvironment;
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpGet("{urlpath}")]
        public async Task<Applicationpage> Get(string urlpath)
        {
            var page = await this.applicationdbcontext.Applicationpages.FirstOrDefaultAsync(item => item.Urlpath == urlpath);
            return page;
        }

        [AllowAnonymous]
        [HttpGet("bysideid/{siteid}")]
        public async Task<List<Applicationpage>> Bysideid(string siteid)
        {
            var items = await this.applicationdbcontext.Applicationpages.Where(item => item.Siteid == siteid).ToListAsync();
            return items;
        }

        [AllowAnonymous]
        [HttpGet("{contextpage}/{itemsperpage}/{siteid}")]
        public async Task<Pagerapiitem<Applicationpage>> Get(int contextpage, int itemsperpage, string siteid)
        {
            var totalitems = this.applicationdbcontext.Applicationpages.Where(item => item.Siteid == siteid);
            var items = await totalitems.Skip((contextpage - 1) * itemsperpage).Take(itemsperpage).ToListAsync();
            int pagesTotal = Convert.ToInt32(Math.Ceiling(totalitems.Count() / Convert.ToDouble(itemsperpage)));

            var apiitem = new Pagerapiitem<Applicationpage>()
            {
                Items = items,
                Pages = pagesTotal
            };

            return apiitem;
        }

        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] Applicationpage page)
        {
            var existingpage = await this.applicationdbcontext.Applicationpages.FirstOrDefaultAsync(item => item.Id == page.Id);
            if (existingpage == null)
            {
                await this.applicationdbcontext.Applicationpages.AddAsync(page);
                await this.applicationdbcontext.SaveChangesAsync();
            }
        }

        [Authorize]
        [HttpPut]
        public async Task Put([FromBody] Applicationpage page)
        {
            var existingpage = await this.applicationdbcontext.Applicationpages.FirstOrDefaultAsync(item => item.Id == page.Id);
            if (existingpage != null)
            {
                existingpage.Name = page.Name;
                existingpage.Urlpath = page.Name.ToLower().Replace(" ", "");

                this.applicationdbcontext.Applicationpages.Update(existingpage);
                await this.applicationdbcontext.SaveChangesAsync();
            }
        }

        [Authorize]
        [HttpDelete("{pageid}")]
        public async Task Delete(string pageid)
        {
            await this.applicationdbcontext.Applicationpages.Where(item => item.Id == pageid).ExecuteDeleteAsync<Applicationpage>();
            await this.applicationdbcontext.SaveChangesAsync();
        }

    }
}
