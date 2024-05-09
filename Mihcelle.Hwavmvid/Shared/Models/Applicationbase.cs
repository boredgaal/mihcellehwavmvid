using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationbase
    {

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [StringLength(410)]
        public string? Id { get; set; }
        public DateTime Createdon { get; set; }

    }
}
