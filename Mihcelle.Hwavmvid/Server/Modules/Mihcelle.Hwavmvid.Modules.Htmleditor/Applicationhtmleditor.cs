using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{
    public class Applicationhtmleditor
    {


        [Key]
        [StringLength(410)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(410)]
        public string Moduleid { get; set; }

        [StringLength(841)]
        public string Htmlstring { get; set; }

        public DateTime Createdon { get; set; }

    }
}
