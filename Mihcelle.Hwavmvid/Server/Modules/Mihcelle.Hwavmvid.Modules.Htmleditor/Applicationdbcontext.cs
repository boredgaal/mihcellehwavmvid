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

                // add html editor to frontpage
                var frontpage = await base.Applicationpages.FirstOrDefaultAsync(item => item.Name == frontpagename);
                var frontpagecontainer = await base.Applicationcontainers.FirstOrDefaultAsync(item => item.Pageid == frontpage.Id);
                var frontpagecontainercolumn = await base.Applicationcontainercolumns.FirstOrDefaultAsync(item => item.Containerid == frontpagecontainer.Id);

                if (frontpage != null && frontpage.Name == frontpagename)
                {

                    var htmlmodule = new Applicationmodule()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Packageid = Guid.NewGuid().ToString(),
                        Containercolumnid = frontpagecontainercolumn.Id,
                        Assemblytype = "Mihcelle.Hwavmvid.Modules.Htmleditor.Index, Mihcelle.Hwavmvid.Client",
                        Settingstype = "Mihcelle.Hwavmvid.Modules.Htmleditor.Settings, Mihcelle.Hwavmvid.Client",
                        Containercolumnposition = 0,
                        Createdon = DateTime.Now,
                    };

                    await base.Applicationmodules.AddAsync(htmlmodule);
                    await base.SaveChangesAsync();

                    string htmlstring = $"<div class=\"row\">\n" + 
                                            
                                            $"<div class=\"col-sm-4\">\n" +
                                                $"\t<img src=\"/medialibrary/west_virginia.png\" title=\"mihcellehwavmvid\" alt=\"whenever mihcellehwavmvid\" class=\"img-fluid\" />\n" +
                                            $"</div>\n" +

                                            $"<div class=\"col-sm-8\">\n" +
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

                    var htmleditor = new Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationhtmleditor()
                    {
                        Moduleid = htmlmodule.Id,
                        Htmlstring = htmlstring,
                        Createdon = DateTime.Now,
                    };

                    await this.Applicationhtmleditors.AddAsync(htmleditor);
                    await this.SaveChangesAsync();
                }
            }
            catch (Exception message)
            {
                Console.WriteLine(message);
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
