using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationuser : IdentityUser
    {

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [StringLength(410)]
        public override string Id { get; set; }

    }
}
