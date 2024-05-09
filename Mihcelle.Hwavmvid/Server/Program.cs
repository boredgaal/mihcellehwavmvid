using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Shared.Constants;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// mihcelle.hwavmvid
var configbuilder = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.ContentRootPath}.json", true, true);
var config = configbuilder.Build();

// mihcelle.hwavmvid
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var installed = !string.IsNullOrEmpty(connectionString);

if (installed == false)
{
    var configpath = string.Concat(builder.Environment.ContentRootPath, "\\wwwroot\\", "tobaccoindustries.json");
    var jsonconfig = System.IO.File.ReadAllText(configpath);
    var deserializedconfig = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonconfig);
    if (deserializedconfig != null)
    {
        deserializedconfig["installation"] = new { createdon = string.Empty };
        var updatedconfigfile = JsonSerializer.Serialize(deserializedconfig, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(configpath, updatedconfigfile);
    }
}

try
{
    builder.Services.AddDbContext<Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddIdentity<Applicationuser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 2;
    })
        .AddEntityFrameworkStores<Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext>();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.Name = Authentication.Authcookiename;
        options.Cookie.HttpOnly = false;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });
} catch (Exception exception) { Console.WriteLine(exception.Message); }

builder.Services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.JsonSerializerOptions.DefaultBufferSize = 4096;
                options.JsonSerializerOptions.MaxDepth = 41;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IHostedService, Mihcelle.Hwavmvid.Server.Tasks.Hostedservicebase>();


builder.Services.AddCors(option =>
{
    option.AddPolicy("mihcellehwavmvidcorspolicy", (builder) =>
    {
        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
               .SetIsOriginAllowed(isOriginAllowed => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// mihcelle.hwavmvid
builder.Services.AddSignalR()
    .AddHubOptions<Applicationhub>(options =>
    {
        options.EnableDetailedErrors = true;
        options.KeepAliveInterval = TimeSpan.FromSeconds(15);
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        options.MaximumReceiveMessageSize = Int64.MaxValue;
        options.StreamBufferCapacity = Int32.MaxValue;
    })
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.WriteIndented = false;
        options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
        options.PayloadSerializerOptions.AllowTrailingCommas = true;
        options.PayloadSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
        options.PayloadSerializerOptions.DefaultBufferSize = 4096;
        options.PayloadSerializerOptions.MaxDepth = 41;
        options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });


if (installed == true)
{
    try
    {
        var programitems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(Programinterface)).IsAssignableFrom(assemblytypes));

        programitems = programitems.Where(item => item.IsClass);
        programitems = programitems.OrderBy(item => !string.IsNullOrEmpty(item.FullName) && item.FullName.StartsWith("Mihcelle.Hwavmvid.Programstartup")).ToList();
        
        foreach (var item in programitems)
        {
            Programinterface? programinterfaceinstance = (Programinterface?) Activator.CreateInstance(item);
            if (programinterfaceinstance != null)
                programinterfaceinstance.Configure(builder.Services);
        }
    } 
    catch (Exception exception) { Console.WriteLine(exception.Message); }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("mihcellehwavmvidcorspolicy");

app.UseAuthentication();
app.UseAuthorization();

// mihcelle.hwavmvid
app.MapHub<Applicationhub>("/api/applicationhub", options =>
    {
        options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
        options.ApplicationMaxBufferSize = long.MaxValue;
        options.TransportMaxBufferSize = long.MaxValue;
        options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);
        options.LongPolling.PollTimeout = TimeSpan.FromSeconds(10);
    });

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

try
{
    var programitems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(assemblytypes => (typeof(Programinterface)).IsAssignableFrom(assemblytypes));
    programitems = programitems.Where(item => item.IsClass);
    programitems = programitems.OrderBy(item => !string.IsNullOrEmpty(item.FullName) && item.FullName.StartsWith("Mihcelle.Hwavmvid.Programstartup")).ToList();

    foreach (var item in programitems)
    {
        Programinterface? programinterfaceinstance = (Programinterface?)Activator.CreateInstance(item);
        if (programinterfaceinstance != null)
            programinterfaceinstance.Configureapp(app);
    }
}
catch (Exception exception) { Console.WriteLine(exception.Message); }

app.Run();


