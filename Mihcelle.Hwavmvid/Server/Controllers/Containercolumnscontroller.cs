using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Containercolumnscontroller : ControllerBase
    {

        public Applicationdbcontext applicationdbcontext { get; set; }

        public Containercolumnscontroller(Applicationdbcontext applicationdbcontext) 
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpGet("{containerid}")]
        public async Task<List<Applicationcontainercolumn>> Get(string containerid)
        {

            var columns = await this.applicationdbcontext.Applicationcontainercolumns.Where(item => item.Containerid == containerid).ToListAsync();
            if (columns != null && columns.Any())
            {
                var orderedcolumns = columns.OrderBy(item => item.Gridposition).ToList();
                foreach (var column in orderedcolumns)
                {
                    column.Modules.Clear();
                    var modules = await this.applicationdbcontext.Applicationmodules.Where(item => item.Containercolumnid == column.Id).ToListAsync();
                    foreach (var module in modules)
                    {
                        column.Modules.Add(module);
                    }

                    column.Modules = column.Modules.OrderBy(item => item.Createdon).ToList();
                }

                return orderedcolumns;
            }

            return new List<Applicationcontainercolumn>();
        }

    }
}
