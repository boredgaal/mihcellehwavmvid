using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationcontainercolumn : Applicationbase
    {

        [StringLength(410)]
        public string Containerid { get; set; }
        public int Gridposition { get; set; }
        public string Columnwidth { get; set; }


        [NotMapped] public List<Applicationmodule> Modules { get; set; } = new List<Applicationmodule>();
        [NotMapped] public IJSObjectReference? JSObjectReference { get; set; }

    }
}
