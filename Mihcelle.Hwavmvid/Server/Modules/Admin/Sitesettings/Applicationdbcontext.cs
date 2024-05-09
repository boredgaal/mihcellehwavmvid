using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Shared.Constants;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Framework.Modules.Sitesettings
{

    public class Applicationdbcontext : IModuleinsideframeworkinstallerinterface
    {

        public Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext _frameworkdb { get; set; }

        public Applicationdbcontext(Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext _frameworkdb)
        {
            this._frameworkdb = _frameworkdb;
        }

        public async Task Removemodule(string moduleid)
        {
            await this._frameworkdb.Applicationmodulesettings.Where(item => item.Moduleid == moduleid).ExecuteDeleteAsync();
            await this._frameworkdb.SaveChangesAsync();            
        }
        
        public Applicationmodulepackage applicationmodulepackage 
        {
            get
            {
                var package = new Applicationmodulepackage()
                {
                    Name = "Sitesettings",
                    Version = "1.0.0",
                    Icon = "S",
                    Assemblytype = "Mihcelle.Hwavmvid.Modules.Sitesettings.Sitesettings, Mihcelle.Hwavmvid.Client",
                    Settingstype = "Mihcelle.Hwavmvid.Modules.Sitesettings.Settings, Mihcelle.Hwavmvid.Client",
                    Description = Applicationmoduletype.Applicationmoduleframework,
                    Createdon = DateTime.Now,
                };

                return package;
            }
        }

    }
}
