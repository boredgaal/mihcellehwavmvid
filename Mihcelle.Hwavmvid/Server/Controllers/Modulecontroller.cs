using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Modules.Htmleditor;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Modulecontroller : ControllerBase
    {

        public IServiceProvider serviceprovider { get; set; }
        public Applicationdbcontext applicationdbcontext { get; set; }
        public Modulecontroller(IServiceProvider serviceprovider, Applicationdbcontext applicationdbcontext)
        {
            this.serviceprovider = serviceprovider;
            this.applicationdbcontext = applicationdbcontext;
        }

        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] Applicationmodule module)
        {
            await this.applicationdbcontext.Applicationmodules.AddAsync(module);
            await this.applicationdbcontext.SaveChangesAsync();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {

            var module = await this.applicationdbcontext.Applicationmodules.FirstOrDefaultAsync(item => item.Id == id);
            if (module != null)
            {
                var installeritems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(IModuleinstallerinterface)).IsAssignableFrom(assemblytypes));
                foreach (var item in installeritems)
                {
                    if (item.IsClass)
                    {
                        var moduleinstaller = (IModuleinstallerinterface?)this.serviceprovider.GetService(item);
                        if (moduleinstaller != null)
                        {
                            try {
                                await moduleinstaller.Removemodule(id);
                            }
                            catch (Exception exception) { Console.WriteLine(exception.Message); }
                        }
                    }
                }

                await this.applicationdbcontext.Applicationmodules.Where(item => item.Id == id).ExecuteDeleteAsync();
                await this.applicationdbcontext.SaveChangesAsync();
            }
        }

    }
}
