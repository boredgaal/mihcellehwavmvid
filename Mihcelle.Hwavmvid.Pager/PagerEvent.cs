namespace Mihcelle.Hwavmvid.Pager
{
    public class Pagerevent<TPagerItem>
    {

        public string ApiQueryId { get; set; }
        public TPagerItem Item { get; set; }

    }
}
