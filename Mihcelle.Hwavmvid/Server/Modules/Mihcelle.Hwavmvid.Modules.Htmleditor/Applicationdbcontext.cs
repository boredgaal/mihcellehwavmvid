using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{

    public class Applicationdbcontext : Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext, IModuleinstallerinterface
    {

        public DbSet<Applicationhtmleditor> Applicationhtmleditors { get; set; }
        private const string frontpagename = "Mihcellehwavmvid corporatcc";

        public Applicationdbcontext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try { base.OnModelCreating(builder); } catch { }
        }

        public async Task Removemodule(string moduleid)
        {
            await this.Applicationhtmleditors.Where(item => item.Moduleid == moduleid).ExecuteDeleteAsync();
            await this.SaveChangesAsync();            
        }

        public async Task Install()
        {
            try
            {
                // migrate html editor database
                await this.Database.MigrateAsync();
            }
            catch (Exception message)
            {
                Console.WriteLine(message);
            }
        }

        public async Task Installed(Applicationmodulepackage installedmodulepackage)
        {

            var frontpage = await base.Applicationpages.FirstOrDefaultAsync(item => item.Name == frontpagename);
            var frontpagecontainer = await base.Applicationcontainers.FirstOrDefaultAsync(item => item.Pageid == frontpage.Id);
            var frontpagecontainercolumn = await base.Applicationcontainercolumns.FirstOrDefaultAsync(item => item.Containerid == frontpagecontainer.Id);

            if (frontpage != null && frontpage.Name == frontpagename)
            {

                var htmlmodule_1 = new Applicationmodule()
                {
                    Id = Guid.NewGuid().ToString(),
                    Packageid = installedmodulepackage.Id,
                    Containercolumnid = frontpagecontainercolumn.Id,
                    Assemblytype = installedmodulepackage.Assemblytype,
                    Settingstype = installedmodulepackage.Settingstype,
                    Containercolumnposition = 0,
                    Createdon = DateTime.Now,
                };

                await base.Applicationmodules.AddAsync(htmlmodule_1);
                await base.SaveChangesAsync();

                var htmlmodule_2 = new Applicationmodule()
                {
                    Id = Guid.NewGuid().ToString(),
                    Packageid = installedmodulepackage.Id,
                    Containercolumnid = frontpagecontainercolumn.Id,
                    Assemblytype = installedmodulepackage.Assemblytype,
                    Settingstype = installedmodulepackage.Settingstype,
                    Containercolumnposition = 0,
                    Createdon = DateTime.Now,
                };

                await base.Applicationmodules.AddAsync(htmlmodule_2);
                await base.SaveChangesAsync();

                string htmlstring_1 = $"<div class=\"row\">\n" +

                                            $"<div class=\"col-sm-3\">\n" +
                                                $"\t<img src=\"/medialibrary/west_virginia.png\" title=\"mihcellehwavmvid\" alt=\"whenever mihcellehwavmvid\" class=\"img-fluid\" />\n" +
                                            $"</div>\n" +

                                            $"<div class=\"col-sm-9\">\n" +
                                                $"\t<h1>Developan Perequisites</ h1>\n" +
                                                $"\t<hr />\n" +
                                                $"\t<table class=\"table table-sm\" style=\"font-size: 14px;\">\n" +
                                                    $"\t<thead></thead>\n" +
                                                    $"\t<tbody>\n" +
                                                        $"<tr><td>asp .net core 7.0.2 sdk & hosting bundle 7.0.2</td></tr>\n" +
                                                        $"<tr><td>install latest vs & mssql & smss</td></tr>\n" +
                                                        $"<tr><td>iis [all] features</td></tr>\n" +
                                                        $"<tr><td>iis web deploy download</td></tr>\n" +
                                                        $"<tr><td>iis url rewrite module</td></tr>\n" +
                                                        $"<tr><td>edit permissions app pool id & iis_iusrs & network service</td></tr>\n" +
                                                        $"<tr><td>update hosts config file for desktop .cmd url and update /server/properties/launhcsettings.json</td></tr>\n" +
                                                        $"<tr><td></td></tr>\n" +
                                                    $"\t</tbody>\n" +
                                                $"\t</table>\n" +
                                            $"</div>\n" +

                                        $"</div>\n";

                string htmlstring_2 = $"<div class=\"row\">\n" +

                                            $"<div class=\"col-sm-3\">\n" +
                                                $"\t<img src=\"/medialibrary/delaware_city.png\" title=\"mihcellehwavmvid\" alt=\"whenever mihcellehwavmvid\" class=\"img-fluid\" />\n" +
                                            $"</div>\n" +

                                            $"<div class=\"col-sm-9\">\n" +

                                                $"<h1>in memory all them victims that died in war 1541 in middle europe</h1>\n" +
                                                $"<div>nothing against the reasonable scripture see byfar the constitution of the united states of america inbetween northcarolina annd westvirginia</div>\n" +
                                                $"<div>go die: on the highways</div>\n" +

                                            $"</div>\n" +

                                        $"</div>\n";


                var htmleditor_1 = new Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationhtmleditor()
                {
                    Moduleid = htmlmodule_1.Id,
                    Htmlstring = htmlstring_1,
                    Createdon = DateTime.Now,
                };

                await this.Applicationhtmleditors.AddAsync(htmleditor_1);
                await this.SaveChangesAsync();

                var htmleditor_2 = new Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationhtmleditor()
                {
                    Moduleid = htmlmodule_2.Id,
                    Htmlstring = htmlstring_2,
                    Createdon = DateTime.Now,
                };

                await this.Applicationhtmleditors.AddAsync(htmleditor_2);
                await this.SaveChangesAsync();
            }

        }

        public async Task Deinstall()
        {
            await this.Database.RollbackTransactionAsync();
        }
        
        public Applicationmodulepackage applicationmodulepackage 
        {
            get
            {
                var package = new Applicationmodulepackage()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Htmleditor",
                    Version = "1.0.0",
                    Icon = "I",
                    Assemblytype = "Mihcelle.Hwavmvid.Modules.Htmleditor.Index, Mihcelle.Hwavmvid.Client",
                    Settingstype = "Mihcelle.Hwavmvid.Modules.Htmleditor.Settings, Mihcelle.Hwavmvid.Client",
                    Description = string.Empty,
                    Createdon = DateTime.Now,
                };

                return package;
            }
        }

    }
}
