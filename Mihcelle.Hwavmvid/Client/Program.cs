using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Mihcelle.Hwavmvid.Client;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net.Mime;
using Mihcelle.Hwavmvid.Cookies;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Modal;
using Mihcelle.Hwavmvid.Fileupload;
using Mihcelle.Hwavmvid.Pager;
using Mihcelle.Hwavmvid;
using Mihcelle.Hwavmvid.Alerts;
using Mihcelle.Hwavmvid.Notifications;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// mihcelle.hwavmvid
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// mihcelle.hwavmvid
builder.Services.AddScoped<AuthenticationStateProvider, Applicationauthenticationstateprovider>();
builder.Services.AddScoped<Applicationprovider, Applicationprovider>();

// mihcelle.hwavmvid
builder.Services.AddHttpClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated",
    client => { client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api"); });

// mihcelle.hwavmvid
var configclient = new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(serviceprovider => configclient);
using var response = await configclient.GetAsync("tobaccoindustries.json");
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(stream);

// mihcelle.hwavmvid
builder.Services.AddScoped<Modalservice, Modalservice>();
builder.Services.AddScoped<Fileuploadservice, Fileuploadservice>();
builder.Services.AddScoped<Cookiesprovider, Cookiesprovider>();
builder.Services.AddScoped<Pagerservice<Applicationpage>, Pagerservice<Applicationpage>>();
builder.Services.AddScoped<Pagerservice<Applicationtask>, Pagerservice<Applicationtask>>();
builder.Services.AddScoped<Pagerservice<Applicationmediafile>, Pagerservice<Applicationmediafile>>();
builder.Services.AddScoped<Applicationmodulesettingsservice, Applicationmodulesettingsservice>();
builder.Services.AddScoped<AlertsService, AlertsService>();
builder.Services.AddScoped<NotificationsService, NotificationsService>();


try
{
    var programitems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(Programinterfaceclient)).IsAssignableFrom(assemblytypes));
    foreach (var item in programitems)
    {
        if (item.IsClass)
        {
            Programinterfaceclient? programinterfaceinstance = (Programinterfaceclient?)Activator.CreateInstance(item);
            if (programinterfaceinstance != null)
                programinterfaceinstance.Configure(builder.Services);
        }
    }
}
catch (Exception exception) { Console.WriteLine(exception.Message); }

WebAssemblyHost host = builder.Build();
IConfiguration? configuration = host.Services.GetService<IConfiguration>();
Cookiesprovider? cookiesprovider = host.Services.GetService<Cookiesprovider>();
if (cookiesprovider != null && string.IsNullOrEmpty(configuration?["installation:createdon"]))
{
    await cookiesprovider.Initcookiesprovider();
    await cookiesprovider.Setcookie(Mihcelle.Hwavmvid.Shared.Constants.Authentication.Authcookiename, string.Empty, (-1));
}

await host.RunAsync();