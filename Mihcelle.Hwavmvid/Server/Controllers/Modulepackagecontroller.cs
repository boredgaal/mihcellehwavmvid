using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Modules.Htmleditor;
using Mihcelle.Hwavmvid.Shared.Constants;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Modulepackagecontroller : ControllerBase
    {

        public Applicationdbcontext applicationdbcontext { get; set; }
        public IServiceProvider servicescopefactory { get; set; }

        public Modulepackagecontroller(Applicationdbcontext applicationdbcontext, IServiceProvider servicescopefactory)
        {
            this.applicationdbcontext = applicationdbcontext;
            this.servicescopefactory = servicescopefactory;
        }

        [AllowAnonymous]
        [HttpGet("packageextensions")]
        public async Task<List<Applicationmodulepackage>> packageextensions()
        {
            var items = await this.applicationdbcontext.Applicationmodulepackages.Where(item => !item.Description.StartsWith(Applicationmoduletype.Applicationmoduleframework)).ToListAsync();
            return items.OrderBy(item => item.Name).ToList();
        }

        [AllowAnonymous]
        [HttpGet("frameworkpackages")]
        public async Task<List<Applicationmodulepackage>?> frameworkpackages()
        {

            var items = await this.applicationdbcontext.Applicationmodulepackages.Where(item => item.Description.StartsWith(Applicationmoduletype.Applicationmoduleframework)).ToListAsync();
            return items.OrderBy(item => item.Name).ToList();

        }

    }
}
