using BlazorDraggableList;
using BlazorDropdown;
using BlazorDynamicLayout;
using BlazorSelect;
using BlazorSlider;
using BlazorTabMenu;
using BlazorTabs;
using Mihcelle.Hwavmvid.Accordion;
using Mihcelle.Hwavmvid.Alerts;
using Mihcelle.Hwavmvid.BrowserResize;
using Mihcelle.Hwavmvid.ColorPicker;
using Mihcelle.Hwavmvid.Devices;
using Mihcelle.Hwavmvid.Download;
using Mihcelle.Hwavmvid.Jsapigeolocation;
using Mihcelle.Hwavmvid.Jsapinotifications;
using Mihcelle.Hwavmvid.Video;
using Mihcelle.Hwavmvid.VideoPlayer;
using Mihcelle.Hwavmvid.Fileupload;
using Mihcelle.Hwavmvid.Modal;
using Mihcelle.Hwavmvid.Pager;
using Oqtane.ChatHubs.Models;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterfaceclient
    {

        public void Configure(IServiceCollection services)
        {

            try
            {

                services.AddScoped<Services.ChatHubService, Services.ChatHubService>();
                services.AddScoped<Services.ScrollService, Services.ScrollService>();
                services.AddScoped<BlazorDraggableListService, BlazorDraggableListService>();
                services.AddScoped<BlazorDynamicLayoutService, BlazorDynamicLayoutService>();
                services.AddScoped<BlazorSliderService, BlazorSliderService>();
                services.AddScoped<BrowserResizeService, BrowserResizeService>();
                services.AddScoped<ColorPickerService, ColorPickerService>();
                services.AddScoped<DevicesService, DevicesService>();
                services.AddScoped<DownloadService, DownloadService>();
                services.AddScoped<Jsapigeolocationservice, Jsapigeolocationservice>();
                services.AddScoped<Jsapibingmapservice, Jsapibingmapservice>();
                services.AddScoped<JsapinotificationService, JsapinotificationService>();
                services.AddScoped<VideoService, VideoService>();
                services.AddScoped<VideoPlayerService, VideoPlayerService>();

                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubRoom>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubRoom>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubUser>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubUser>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubCam>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubCam>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubInvitation>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubInvitation>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnore>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnore>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnoredBy>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnoredBy>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubModerator>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubModerator>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubBlacklistUser>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubBlacklistUser>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubWhitelistUser>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubWhitelistUser>>();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

    }
}
