using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas
{
    public class PhoneNum
    {
        [Required]
        public string Num { get; set; }
        [Required]
        public bool Confirmed { get; set; }
    }
}
