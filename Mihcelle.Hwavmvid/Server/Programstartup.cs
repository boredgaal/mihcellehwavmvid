using Mihcelle.Hwavmvid.Server;

namespace Mihcelle.Hwavmvid
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterface
    {

        public async Task Configure(IServiceCollection services)
        {
            services.AddScoped<Mihcelle.Hwavmvid.Framework.Modules.Databasesettings.Applicationdbcontext, Mihcelle.Hwavmvid.Framework.Modules.Databasesettings.Applicationdbcontext>();
            services.AddScoped<Mihcelle.Hwavmvid.Framework.Modules.Pagesettings.Applicationdbcontext, Mihcelle.Hwavmvid.Framework.Modules.Pagesettings.Applicationdbcontext>();
            services.AddScoped<Mihcelle.Hwavmvid.Framework.Modules.Sitesettings.Applicationdbcontext, Mihcelle.Hwavmvid.Framework.Modules.Sitesettings.Applicationdbcontext>();
            services.AddScoped<Mihcelle.Hwavmvid.Framework.Modules.Taskmanager.Applicationdbcontext, Mihcelle.Hwavmvid.Framework.Modules.Taskmanager.Applicationdbcontext>();
            services.AddScoped<Mihcelle.Hwavmvid.Framework.Modules.Mediafiles.Applicationdbcontext, Mihcelle.Hwavmvid.Framework.Modules.Mediafiles.Applicationdbcontext>();
        }

        public async Task Configureapp(WebApplication application)
        {

            try // run modules installer migrate database and add package references to database
            {

                var installeritems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(IModuleinstallerinterface)).IsAssignableFrom(assemblytypes));
                foreach (var item in installeritems)
                {
                    if (item.IsClass)
                    {
                        using (var scope = application.Services.CreateScope())
                        {
                            var moduleinstaller = (IModuleinstallerinterface?)scope.ServiceProvider.GetService(item);
                            if (moduleinstaller != null)
                            {
                                var package = moduleinstaller.applicationmodulepackage;
                                var dbcontext = scope.ServiceProvider.GetService<Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext>();

                                if (dbcontext != null)
                                {
                                    var installedpackage = dbcontext.Applicationmodulepackages.Where(item => item.Name == package.Name).FirstOrDefault();
                                    if (installedpackage == null)
                                    {

                                        await moduleinstaller.Install();
                                        await dbcontext.Applicationmodulepackages.AddAsync(package);
                                        await moduleinstaller.Installed(package);
                                        await dbcontext.SaveChangesAsync();

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }

            try // run modules installer and add package references to database
            {
                var installeritems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(IModuleinsideframeworkinstallerinterface)).IsAssignableFrom(assemblytypes));
                foreach (var item in installeritems)
                {
                    if (item.IsClass)
                    {
                        using (var scope = application.Services.CreateScope())
                        {
                            var moduleinstaller = (IModuleinsideframeworkinstallerinterface?)scope.ServiceProvider.GetService(item);
                            if (moduleinstaller != null)
                            {
                                var package = moduleinstaller.applicationmodulepackage;
                                var dbcontext = scope.ServiceProvider.GetService<Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext>();

                                if (dbcontext != null)
                                {
                                    var installedpackage = dbcontext.Applicationmodulepackages.Where(item => item.Name == package.Name).FirstOrDefault();
                                    if (installedpackage == null)
                                    {

                                        dbcontext.Applicationmodulepackages.Add(package);
                                        await dbcontext.SaveChangesAsync();

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }

        }

    }
}
