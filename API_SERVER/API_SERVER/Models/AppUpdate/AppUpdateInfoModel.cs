using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_SERVER.Models
{
    public class AppUpdateInfoModel
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int versionCode { get; set; }
        public string url { get; set; }
        public string versionName { get; set; }
        public string content { get; set; }
    }
}
