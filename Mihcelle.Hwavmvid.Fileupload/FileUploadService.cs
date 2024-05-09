using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Fileupload
{
    public class Fileuploadservice
    {

        public IJSRuntime JSRuntime;
        public IJSObjectReference Module;
        public IJSObjectReference FileUploadMap;

        public FileUploadServiceExtension FileUploadServiceExtension;

        public DotNetObjectReference<FileUploadServiceExtension> dotNetObjectReference;

        public Fileuploadservice(IJSRuntime jsRuntime)
        {
            this.JSRuntime = jsRuntime;
            this.FileUploadServiceExtension = new FileUploadServiceExtension();
            this.dotNetObjectReference = DotNetObjectReference.Create(this.FileUploadServiceExtension);
        }

        public async Task InitFileUploadDropzone(string inputFileId, string elementId)
        {
            this.Module = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/mihcelle.hwavmvid.fileupload/blazorfileuploadjsinterop.js");
            this.FileUploadMap = await this.Module.InvokeAsync<IJSObjectReference>("initfileupload", this.dotNetObjectReference, inputFileId, elementId);
        }

    }

    public class FileUploadServiceExtension
    {

        public event EventHandler<Fileuploadevent> OnDropEvent;

        [JSInvokable("OnDrop")]
        public void OnDrop(string elementId)
        {
            Fileuploadevent eventParameters = new Fileuploadevent() { FileUploadDropzoneId = elementId };
            OnDropEvent?.Invoke(this, eventParameters);
        }

    }
}
