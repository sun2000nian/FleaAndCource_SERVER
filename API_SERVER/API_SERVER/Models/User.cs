using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models
{
    public class User
    {
        [Required]
        [Key]
        public string UserID { get; set; }
    }
}
