using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mihcelle.Hwavmvid.Modules.Blackjack;

namespace Mihcelle.Hwavmvid.Server.Modules.Blackjack
{
    [ApiController]
    [Route("api/[controller]")]
    public class Blackjackcontroller
    {

        public Applicationdbcontext applicationdbcontext { get; set; }

        public Blackjackcontroller(Applicationdbcontext applicationdbcontext)
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] Applicationblackjack editor)
        {

            var itemexist = this.applicationdbcontext.Applicationblackjacks.FirstOrDefault(item => item.Id == editor.Id);
            if (itemexist != null)
            {
                this.applicationdbcontext.Applicationblackjacks.Update(itemexist);
                this.applicationdbcontext.SaveChanges();
            }
            else
            {
                var item = new Applicationblackjack()
                {
                    Moduleid = editor.Moduleid,
                    Createdon = DateTime.Now,
                };

                this.applicationdbcontext.Applicationblackjacks.Add(item);
                this.applicationdbcontext.SaveChanges();
            }
        }

    }
}
