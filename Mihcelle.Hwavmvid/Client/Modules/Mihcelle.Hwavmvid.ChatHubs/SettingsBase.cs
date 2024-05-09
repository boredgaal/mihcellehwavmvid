using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.ColorPicker;
using Mihcelle.Hwavmvid.Client.Modules;
using Mihcelle.Hwavmvid.Client;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Services;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{
    public class SettingsBase : ComponentBase, IDisposable
    {


        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ChatHubService ChatHubService { get; set; }
        [Inject] public HttpClient httpClient { get; set; }
        [Inject] public Applicationmodulesettingsservice SettingService { get; set; }
        [Inject] public ColorPickerService ColorPickerService { get; set; }

        public string backgroundColor { get; set; }
        public string maxUserNameCharacters { get; set; }
        public string framerate { get; set; }
        public string videoBitsPerSecond { get; set; }
        public string audioBitsPerSecond { get; set; }
        public string videoSegmentsLength { get; set; }
        public string geoLocationPositionInterval { get; set; }
        public string bingMapsApiKey { get; set; }
        public string regularExpression { get; set; }

        public List<string> regularExpressions = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.ColorPickerService.OnColorPickerContextColorChangedEvent += this.OnColorPickerChangeExecute;

                Dictionary<string, string> settings = await this.SettingService.GetModuleSettingsAsync(this.ChatHubService.ModuleId);
                this.backgroundColor = this.SettingService.GetSetting(settings, "BackgroundColor", "#f0f0f0");
                this.maxUserNameCharacters = this.SettingService.GetSetting(settings, "MaxUserNameCharacters", "20");
                this.framerate = this.SettingService.GetSetting(settings, "Framerate", "24");
                this.videoBitsPerSecond = this.SettingService.GetSetting(settings, "VideoBitsPerSecond", "14000");
                this.audioBitsPerSecond = this.SettingService.GetSetting(settings, "AudioBitsPerSecond", "12800");
                this.videoSegmentsLength = this.SettingService.GetSetting(settings, "VideoSegmentsLength", "2400");
                this.geoLocationPositionInterval = this.SettingService.GetSetting(settings, "GeoLocationPositionInterval", "41000");
                this.bingMapsApiKey = this.SettingService.GetSetting(settings, "BingMapsApiKey", "");
                this.regularExpressions = this.SettingService.GetSetting(settings, "RegularExpression", "").Split(";delimiter;", StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            catch (Exception exception) { 
                Console.WriteLine(exception.Message); }
        }

        public async Task UpdateSettings()
        {
            try
            {

                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "BackgroundColor", this.backgroundColor);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "MaxUserNameCharacters", this.maxUserNameCharacters);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "Framerate", this.framerate);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "VideoBitsPerSecond", this.videoBitsPerSecond);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "AudioBitsPerSecond", this.audioBitsPerSecond);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "VideoSegmentsLength", this.videoSegmentsLength);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "GeoLocationPositionInterval", this.geoLocationPositionInterval);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "BingMapsApiKey", this.bingMapsApiKey);
                this.SettingService.SetSetting(this.ChatHubService.ModuleId, "RegularExpression", string.Join(";delimiter;", regularExpressions));
                
                this.NavigationManager.NavigateTo(this.NavigationManager.Uri, true);
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }
        }

        public void AddRegularExpression_ClickedAsync()
        {
            try
            {
                this.regularExpressions.Add(regularExpression);
                this.regularExpression = string.Empty;
            }
            catch (Exception exception) { }
        }
        public void RemoveRegularExpression_ClickedAsync(string item)
        {
            try
            {
                this.regularExpressions.Remove(item);
            }
            catch (Exception exception) { }
        }

        private void OnColorPickerChangeExecute(ColorPickerEvent obj)
        {
            this.backgroundColor = obj.ContextColor;
        }

        public void Dispose()
        {
            this.ColorPickerService.OnColorPickerContextColorChangedEvent -= this.OnColorPickerChangeExecute;
        }

    }
}
