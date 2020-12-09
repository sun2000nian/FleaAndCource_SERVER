using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API_SERVER.Models
{
    public class UserData_ServerSide
    {
        [Required]
        [Key]
        public string userID { get; set; }
        public string AvatarFileName { get; set; }

    }
}
