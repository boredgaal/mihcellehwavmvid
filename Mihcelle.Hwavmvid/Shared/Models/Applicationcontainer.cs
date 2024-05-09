using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationcontainer : Applicationbase
    {

        [StringLength(410)]
        public string Pageid { get; set; }
        public string Containertype { get; set; }

    }
}
