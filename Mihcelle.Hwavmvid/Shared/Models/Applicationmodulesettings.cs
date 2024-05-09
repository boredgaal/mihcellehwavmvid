using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationmodulesettings : Applicationbase
    {

        [StringLength(410)]
        public string Moduleid { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
