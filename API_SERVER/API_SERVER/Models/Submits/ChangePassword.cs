using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Submits
{
    public class ChangePassword
    {
        public string userID { get; set; }
        public string newPassword { get; set; }
        public string oldPassword { get; set; }
    }
}
