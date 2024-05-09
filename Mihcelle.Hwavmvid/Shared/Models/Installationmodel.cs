using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Installationmodel
    {

        public string Sqlserverinstance { get; set; } = @".\sqlexpress";
        public string Databasename { get; set; } = "mihcelle.hwavmvid";
        public string Databaseowner { get; set; } = "sa";
        public string Databaseownerpassword { get; set; } = "default";
        public string Hostusername { get; } = "host";
        public string Hostpassword { get; set; } = "admin";
        public bool? Usessl { get; } = true;

    }
}
