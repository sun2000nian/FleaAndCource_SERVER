﻿using API_SERVER.Models.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_SERVER.Models.Users
{
    public class PersonalData
    {
        //TODO 改类名为PersonalData
        [Required]
        [Key]
        public string userID { get; set; }
        public string userType { get; set; }

        [DefaultValue(2)]
        public int gender { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string phoneNum { get; set; }
        public string qq { get; set; }
        public string studentID { get; set; }

        public ICollection<CourceModel> courceObjects { get; set; }
        public ICollection<FleaObjectModel> fleaObjects { get; set; }
    }
}