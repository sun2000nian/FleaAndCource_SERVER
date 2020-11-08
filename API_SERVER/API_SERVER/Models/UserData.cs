using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_SERVER.Models
{
    public class UserData
    {
        [Required]
        [Key]
        public string UserID { get; set; }
        public string userType { get; set; }

        [DefaultValue(2)]
        public int gender { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string phoneNum { get; set; }
        public string qq { get; set; }
    }
}
