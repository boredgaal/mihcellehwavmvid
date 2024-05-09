using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Services;
using Mihcelle.Hwavmvid.Client.Modules;
using Mihcelle.Hwavmvid.Modal;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{
    public class SettingsModalBase : Modulebase
    {

        [Inject] public ChatHubService ChatHubService { get; set; }
        [Inject] public Modalservice ModalService { get; set; }

        public const string SettingsModalElementId = "SettingsModalElementId";

        public async void OpenDialogAsync()
        {
            await this.ModalService.ShowModal(SettingsModalElementId);
            StateHasChanged();
        }

        public async void CloseDialogAsync()
        {
            await this.ModalService.HideModal(SettingsModalElementId);
            StateHasChanged();
        }

    }
}
