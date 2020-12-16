using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models
{
    public class AppUpdateInfoModel
    {
        [Key]
        public int versionCode { get; set; }
        public string url { get; set; }
        public string versionName { get; set; }
        public string content { get; set; }
    }
}
