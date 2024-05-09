using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Shared.Constants;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Framework.Modules.Databasesettings
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
                    Name = "Databasesettings",
                    Version = "1.0.0",
                    Icon = "W",
                    Assemblytype = "Mihcelle.Hwavmvid.Modules.Databasesettings.Databasesettings, Mihcelle.Hwavmvid.Client",
                    Settingstype = "Mihcelle.Hwavmvid.Modules.Databasesettings.Settings, Mihcelle.Hwavmvid.Client",
                    Description = Applicationmoduletype.Applicationmoduleframework,
                    Createdon = DateTime.Now,
                };

                return package;
            }
        }

    }
}
