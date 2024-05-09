using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{


    [ApiController]
    [Route("api/[controller]")]
    public class Htmleditorcontroller : ControllerBase
    {

        public Applicationdbcontext applicationdbcontext { get; set; }

        public Htmleditorcontroller(Applicationdbcontext applicationdbcontext) 
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpGet("{moduleid}")]
        public async Task<Applicationhtmleditor> Get(string moduleid)
        {
            var editor = await this.applicationdbcontext.Applicationhtmleditors.FirstOrDefaultAsync(item => item.Moduleid == moduleid);
            return editor;
        }

        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] Applicationhtmleditor editor)
        {

            var itemexist = this.applicationdbcontext.Applicationhtmleditors.FirstOrDefault(item => item.Id == editor.Id);
            if(itemexist != null)
            {
                itemexist.Htmlstring = editor.Htmlstring;
                this.applicationdbcontext.Applicationhtmleditors.Update(itemexist);
                this.applicationdbcontext.SaveChanges();
            }
            else
            {
                var item = new Applicationhtmleditor()
                {
                    Moduleid = editor.Moduleid,
                    Htmlstring = editor.Htmlstring,
                    Createdon = DateTime.Now,
                };

                this.applicationdbcontext.Applicationhtmleditors.Add(item);
                this.applicationdbcontext.SaveChanges();
            }
        }

    }

}
