using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Submits
{
    //HACK:更改类名
    public class LoginSubmit
    {
        [Required]
        public string userID { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
