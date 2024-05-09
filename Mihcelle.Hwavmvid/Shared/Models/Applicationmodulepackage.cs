using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationmodulepackage : Applicationbase
    {

        public string Name { get; set; }
        public string Version { get; set; }
        public string Icon { get; set; }
        public string Assemblytype { get; set; }
        public string Settingstype { get; set; }
        public string Description { get; set; }

        [NotMapped] public IJSObjectReference? JSObjectReference { get; set; }

    }
}
