using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Sitecontroller : ControllerBase
    {


        public IWebHostEnvironment iwebhostenvironment { get; set; }
        public Applicationdbcontext applicationdbcontext { get; set; }

        public Sitecontroller(IWebHostEnvironment iwebhostenvironment, Applicationdbcontext applicationdbcontext)
        {
            this.iwebhostenvironment = iwebhostenvironment;
            this.applicationdbcontext = applicationdbcontext;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<Applicationsite> Get()
        {
            var site = await this.applicationdbcontext.Applicationsites.FirstOrDefaultAsync();
            return site;
        }

        [AllowAnonymous]
        [HttpPost("brandmark")]
        public async Task<IActionResult> Postbrandmark([FromForm] IFormFileCollection files)
        {

            try
            {

                int maximumfilesize = 10;
                int maximumfilesallowed = 1;
                string relativefolderpath = @"\images";
                string absolutefolderpath = string.Concat(this.iwebhostenvironment.WebRootPath, relativefolderpath);

                if (!Directory.Exists(absolutefolderpath))
                {
                    Directory.CreateDirectory(absolutefolderpath);
                }

                if (files.Count > maximumfilesallowed)
                {
                    return new BadRequestObjectResult(new { Message = "Maximum number of files exceeded." });
                }

                foreach (IFormFile file in files)
                {

                    if (file.Length > (maximumfilesize * 1024 * 1024))
                    {
                        return new BadRequestObjectResult(new { Message = "File size Should Be UpTo " + maximumfilesize + "MB" });
                    }

                    var supportedFileExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (!supportedFileExtensions.Contains(fileExtension))
                    {
                        return new BadRequestObjectResult(new { Message = "Unknown file type(s)." });
                    }

                    string fileName = string.Concat(Guid.NewGuid().ToString(), fileExtension);
                    string fullPath = Path.Combine(absolutefolderpath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, true))
                    {
                        file.CopyTo(stream);
                    }

                    var site = await this.applicationdbcontext.Applicationsites.FirstOrDefaultAsync();
                    if (site != null)
                    {
                        site.Brandmark = fileName;
                        this.applicationdbcontext.Applicationsites.Update(site);
                        await this.applicationdbcontext.SaveChangesAsync();
                    }
                }

                return new OkObjectResult(new { Message = "Successfully uploaded brandmark." });
            }
            catch
            {
                return new BadRequestObjectResult(new { Message = "Error uploading brandmark." });
            }

        }

        [AllowAnonymous]
        [HttpPost("favicon")]
        public async Task<IActionResult> Postfavicon([FromForm] IFormFileCollection files)
        {


            try
            {

                int maximumfilesize = 10;
                int maximumfilesallowed = 1;
                //string relativefolderpath = @"\images";
                //string absolutefolderpath = string.Concat(this.iwebhostenvironment.WebRootPath, relativefolderpath);

                /*
                if (!Directory.Exists(absolutefolderpath))
                {
                    Directory.CreateDirectory(absolutefolderpath);
                }
                */

                if (files.Count > maximumfilesallowed)
                {
                    return new BadRequestObjectResult(new { Message = "Maximum number of files exceeded." });
                }

                foreach (IFormFile file in files)
                {

                    if (file.Length > (maximumfilesize * 1024 * 1024))
                    {
                        return new BadRequestObjectResult(new { Message = "File size Should Be UpTo " + maximumfilesize + "MB" });
                    }

                    var supportedFileExtensions = new[] { ".ico" };
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (!supportedFileExtensions.Contains(fileExtension))
                    {
                        return new BadRequestObjectResult(new { Message = "Unknown file type(s)." });
                    }

                    //string fileName = string.Concat(Guid.NewGuid().ToString(), fileExtension);
                    //string fullPath = Path.Combine(absolutefolderpath, fileName);

                    string faviconfullpath = string.Concat(this.iwebhostenvironment.WebRootPath, @"\", "favicon.ico");
                    using (var stream = new FileStream(faviconfullpath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, true))
                    {
                        file.CopyTo(stream);
                    }

                    var site = await this.applicationdbcontext.Applicationsites.FirstOrDefaultAsync();
                    if (site != null)
                    {
                        site.Favicon = "favicon.ico";
                        this.applicationdbcontext.Applicationsites.Update(site);
                        await this.applicationdbcontext.SaveChangesAsync();
                    }
                }

                return new OkObjectResult(new { Message = "Successfully uploaded brandmark." });
            }
            catch
            {
                return new BadRequestObjectResult(new { Message = "Error uploading brandmark." });
            }
        }

    }
}
