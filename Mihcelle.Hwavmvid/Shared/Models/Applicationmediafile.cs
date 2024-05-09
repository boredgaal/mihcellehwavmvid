using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationmediafile : Applicationbase
    {


        [StringLength(410)]
        public string Siteid { get; set; }
        public string Filename { get; set; }
        public string Fileextension { get; set; }
        public long Filesize { get; set; }
        public int Filewidth { get; set; }
        public int Fileheight { get; set; }

    }
}
