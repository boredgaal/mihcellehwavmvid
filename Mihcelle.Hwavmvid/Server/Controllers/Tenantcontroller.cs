using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Modules.Htmleditor;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Tenantcontroller
    {


        public Applicationdbcontext applicationdbcontext { get; set; }

        public Tenantcontroller(Applicationdbcontext applicationdbcontext)
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        [Authorize]
        [HttpGet("{siteid}")]
        public async Task<Applicationtenant> Get(string siteid)
        {
            var tenant = await this.applicationdbcontext.Applicationtenants.FirstOrDefaultAsync(item => item.Siteid == siteid);
            return tenant;
        }

    }
}
