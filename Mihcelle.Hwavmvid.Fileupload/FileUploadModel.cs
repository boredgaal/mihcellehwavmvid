using Microsoft.AspNetCore.Components.Forms;

namespace Mihcelle.Hwavmvid.Fileupload
{
    public class Fileuploadmodel
    {

        public string Base64ImageUrl { get; set; }

        public IBrowserFile BrowserFile { get; set; }

    }
}
