using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationtenant : Applicationbase
    {

        [StringLength(410)]
        public string Siteid { get; set; }
        public string Name { get; set; }
        public string Databaseconnectionstring { get; set; }

    }
}
