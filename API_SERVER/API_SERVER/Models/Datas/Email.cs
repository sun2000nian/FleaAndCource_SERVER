using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas
{
    public class Email
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public bool Confirmed { get; set; }
    }
}
