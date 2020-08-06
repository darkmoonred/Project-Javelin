using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectJavelin.Models
{
    public class Gamertag
    {
        [Display(Name = "Gamertag")]
        public string GamertagName { get; set; }
    }
}
