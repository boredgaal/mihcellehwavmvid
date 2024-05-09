using Mihcelle.Hwavmvid.Modal;
using Microsoft.AspNetCore.Components;
using Oqtane.ChatHubs.Models;
using Mihcelle.Hwavmvid.Client.Modules;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{
    public class ImageModalBase : Modulebase
    {

        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected Modalservice ModalService { get; set; }

        public const string ImageModalElementId = "ImageModalElementId";

        public ChatHubMessage Message { get; set; }

        public ImageModalBase() { }

        public async Task OpenDialogAsync(ChatHubMessage item)
        {
            this.Message = item;
            await this.ModalService.ShowModal(ImageModalElementId);
            StateHasChanged();
        }

        public async Task CloseDialogClickedAsync()
        {
            await this.ModalService.HideModal(ImageModalElementId);
        }

    }
}
