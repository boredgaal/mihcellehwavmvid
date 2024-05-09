using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationtask
    {

        public string Id { get; set; }
        public string Projectname { get; set; }
        public string Taskname { get; set; }
        public bool Active { get; set; }
        public int Interval { get; set; }

    }
}
