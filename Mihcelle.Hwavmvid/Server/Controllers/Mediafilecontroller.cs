using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Pager;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Mediafilecontroller : ControllerBase
    {

        public IWebHostEnvironment iwebhostenvironment { get; set; }
        public Applicationdbcontext applicationdbcontext { get; set; }

        public Mediafilecontroller(IWebHostEnvironment iwebhostenvironment, Applicationdbcontext applicationdbcontext)
        {
            this.iwebhostenvironment = iwebhostenvironment;
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpGet("{contextpage}/{itemsperpage}/{siteid}")]
        public async Task<Pagerapiitem<Applicationmediafile>> Get(int contextpage, int itemsperpage, string siteid)
        {
            var totalitems = this.applicationdbcontext.Applicationmediafiles.Where(item => item.Siteid == siteid);
            var items = await totalitems.Skip((contextpage - 1) * itemsperpage).Take(itemsperpage).ToListAsync();
            int pagesTotal = Convert.ToInt32(Math.Ceiling(totalitems.Count() / Convert.ToDouble(itemsperpage)));

            var apiitem = new Pagerapiitem<Applicationmediafile>()
            {
                Items = items,
                Pages = pagesTotal
            };

            return apiitem;
        }

        [Authorize]
        [HttpPost("images")]
        public async Task<IActionResult> Postimages([FromForm] IFormFileCollection files)
        {

            try
            {

                int maximumfilesize = 10;
                int maximumfilesallowed = 400;
                string relativefolderpath = @"\medialibrary";
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
                    var mediafile = new Applicationmediafile()
                    {
                        Siteid = site.Id,
                        Filename = fileName,
                        Fileextension = fileExtension,
                        Filesize = 0,
                        Filewidth = 0,
                        Fileheight = 0,
                        Createdon = DateTime.Now,
                    };

                    await this.applicationdbcontext.Applicationmediafiles.AddAsync(mediafile);
                    await this.applicationdbcontext.SaveChangesAsync();
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
