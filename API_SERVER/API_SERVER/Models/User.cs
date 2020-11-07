﻿using Microsoft.Extensions.Configuration.UserSecrets;
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
        public Int64 UserID { get; set; }

        public string userType { get; set; }
        public string gender { get; set; }
        public string name { get; set; }

    }
}
